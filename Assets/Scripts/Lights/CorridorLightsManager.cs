using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformLights : MonoBehaviour
{
	public List<Transform> lights;
	public float Radius;
	public float DistBetweenLights;
	public Vector3 Direction;
	public Vector3 firstOffset;
	public Vector2 MinMax;

	private void Start()
	{
		for (int i = 0; i < lights.Count; i++)
		{
			lights[i].position = transform.position + firstOffset + Direction * DistBetweenLights * i;
		}
	}

	private void FixedUpdate()
	{
		for(int i = 0; i < lights.Count; i++)
		{
			if(Vector3.Distance(Controller.current.transform.position, lights[i].position) > Radius)
			{

				Vector3 endPos = lights[i].position + Vector3.Scale(Direction.normalized, (Controller.current.transform.position - lights[i].position)).normalized * DistBetweenLights * lights.Count;
				Vector3 temp = endPos - (transform.position + firstOffset);
				float dist = temp.magnitude * Mathf.Sign(Vector3.Dot(temp, Direction.normalized));


				if (dist < MinMax.y && dist > MinMax.x && Vector3.Distance(Controller.current.transform.position, endPos) <= Radius)
				{
					Debug.Log(dist);
					lights[i].position = endPos;
				}
			}
		}
	}

#if UNITY_EDITOR
	private void OnValidate()
	{
		Start();
	}
#endif
}
