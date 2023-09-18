using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UtilityEditor;
using static UnityEngine.Rendering.HableCurve;
using System.Linq;

public class RailPath : MonoBehaviour
{
	[System.Serializable]
	public class RailNode
	{
		public Vector3 position;
		public Quaternion rotation = Quaternion.identity;
		//[HideInInspector]
		public float length;

		public RailPole PoleA;
		public RailPole PoleB;

		//[HideInInspector]
		public List<Vector2> samples;

		//public int segments;
		public float GetTFromDistance(float distance)
		{
			if (distance >= length) return length;

			int min = 0;
			int max = 0;

			for(int i = 0; i < samples.Count; i++)
			{
				if(distance < samples[i].x)
				{
					max = i;
					break;
				}
				min = i;
			}

			return ((distance - samples[min].x) / (samples[max].x - samples[min].x)) * (samples[max].y - samples[min].y) + samples[min].y;
		}

		[System.Serializable]
		public class RailPole
		{
			public Vector3 position;
			public Quaternion rotation = Quaternion.identity;
		}
	}

	public float length { get {
			float l = 0;
			nodes.ForEach(e => l += e.length);
			return l;
		} }

	public bool closed = false;

	[NonReorderable]
	public List<RailNode> nodes = new List<RailNode>();

	public void RecalculateSegment(int index)
	{
		if(index >= nodes.Count || index == nodes.Count - 1 && !closed) return;

		int nextIndex = (index + 1) % nodes.Count;

		//Debug.Log(index + " " + nextIndex);

		nodes[index].length = SkullAGMaths.GetBezierArcLength(transform.TransformPoint(nodes[index].position), transform.TransformPoint(nodes[nextIndex].position),
							transform.TransformPoint(nodes[index].PoleA.position + nodes[index].position), transform.TransformPoint(nodes[nextIndex].PoleB.position + nodes[nextIndex].position), out nodes[index].samples);
	}

	public float GetTFromDistance(float distance)
	{
		if (distance >= length) return length;
		
		for (int i = 0; i < nodes.Count; i++)
		{
			if(distance > nodes[i].length)
			{
				distance -= nodes[i].length;
			}
			else
			{
				return nodes[i].GetTFromDistance(distance) + i;
			}
		}

		return length;
	}

	public Vector3 GetPoint(float t)
	{
		int i = Mathf.FloorToInt(t);
		int nextIndex = (i + 1) % nodes.Count;
		Debug.Log(i);

		return SkullAGMaths.twoPoleBezierLerp(transform.TransformPoint(nodes[i].position), transform.TransformPoint(nodes[nextIndex].position),
			transform.TransformPoint(nodes[i].PoleA.position + nodes[i].position), transform.TransformPoint(nodes[nextIndex].PoleB.position + nodes[nextIndex].position), t - i);
	}

	public Quaternion GetRotation(float t)
	{
		int i = Mathf.FloorToInt(t);
		int nextIndex = (i + 1) % nodes.Count;

		return SkullAGMaths.QuaternionBezier(transform.rotation * nodes[i].rotation, transform.rotation * nodes[nextIndex].rotation,
			transform.rotation * nodes[i].PoleA.rotation * nodes[i].rotation, transform.rotation * nodes[nextIndex].PoleB.rotation * nodes[nextIndex].rotation, t - i);
	}

	public bool RealTimeUpdate = false;
	public float segmentSize = 0.25f;
	public float SimulationSpeed = 0.5f;
	public float SimDist = 0;

#if UNITY_EDITOR
    [MenuItem("GameObject/Effects/RailPath")]
    static public void CreateNewRailPathObject()
    {
        new GameObject("RailPath").AddComponent<RailPath>().transform.SetParent(Selection.activeTransform);
    }

    private void OnValidate()
	{
		if(RealTimeUpdate)
			for (int i = 0; i < nodes.Count; i++)
			{
				RecalculateSegment(i);
			}
	}

	private void OnDrawGizmos()
	{
		if (!Selection.Contains(this.gameObject)) return;

		Vector3 lastPoint = Vector3.zero;
		Vector3 actualPoint = Vector3.zero;
		int nextIndex = 0;
		int segmentCount = 0;
		float t = 0;
		Quaternion rot = Quaternion.identity;

		if (nodes.Count > 1)
			for (int i = 0; i < nodes.Count; i++)
			{
				Gizmos.color = Color.gray;
				if (i - 1 >= 0 || closed)
				{
					Gizmos.DrawLine(transform.TransformPoint(nodes[i].position), transform.TransformPoint(nodes[i].PoleB.position + nodes[i].position));
					Gizmos.DrawSphere(transform.TransformPoint(nodes[i].PoleB.position + nodes[i].position), 0.2f);
				}

				if(i + 1 < nodes.Count || closed)
				{
					Gizmos.DrawLine(transform.TransformPoint(nodes[i].position), transform.TransformPoint(nodes[i].PoleA.position + nodes[i].position));
					Gizmos.DrawSphere(transform.TransformPoint(nodes[i].PoleA.position + nodes[i].position), 0.2f);
				}

				Gizmos.color = Color.yellow;
				Gizmos.DrawSphere(transform.TransformPoint(nodes[i].position), 0.2f);

				if (i != nodes.Count - 1 || closed)
				{
					nextIndex = (i + 1) % nodes.Count;

					lastPoint = transform.TransformPoint(nodes[i].position);
					segmentCount = Mathf.FloorToInt(nodes[i].length / segmentSize);
					//T = 1f / segmentCount;

					//nodes[i].segments = segmentCount;

					Gizmos.color = Color.blue;
					Gizmos.DrawLine(lastPoint, lastPoint + rot * Vector3.up * 0.2f);

					for (int e = 1; e < segmentCount; e++)
					{
						t = nodes[i].GetTFromDistance(e * segmentSize);


						actualPoint = SkullAGMaths.twoPoleBezierLerp(transform.TransformPoint(nodes[i].position), transform.TransformPoint(nodes[nextIndex].position),
							transform.TransformPoint(nodes[i].PoleA.position + nodes[i].position), transform.TransformPoint(nodes[nextIndex].PoleB.position + nodes[nextIndex].position), t);

						rot = SkullAGMaths.QuaternionBezier(transform.rotation * nodes[i].rotation, transform.rotation * nodes[nextIndex].rotation,
							transform.rotation * nodes[i].PoleA.rotation * nodes[i].rotation, transform.rotation * nodes[nextIndex].PoleB.rotation * nodes[nextIndex].rotation, t);

						Gizmos.color = Color.blue;
						Gizmos.DrawLine(actualPoint, actualPoint + rot * Vector3.up * 0.2f);
						Gizmos.color = Color.green;
						Gizmos.DrawLine(lastPoint, actualPoint);

						lastPoint = actualPoint;
					}

					Gizmos.color = Color.green;
					Gizmos.DrawLine(lastPoint, transform.TransformPoint(nodes[nextIndex].position));
				}

			}

		SimDist = (SimDist + SimulationSpeed * Time.deltaTime) % length;

		Gizmos.DrawCube(GetPoint(GetTFromDistance(SimDist)), Vector3.one * 0.5f);

		/*for (int i = 0; i < 1000; i++)
		{
			Gizmos.DrawLine(Vector3.zero, Vector3.up);
		}*/
	}
#endif
}
