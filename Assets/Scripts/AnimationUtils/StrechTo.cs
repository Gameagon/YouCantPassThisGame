using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class StrechTo : MonoBehaviour
{
	public Transform target;

	public Vector3 offset;


	[SerializeField]
	float originalDist;

	float actualDist;


	public void ResetDist()
	{
		originalDist = Vector3.Distance(transform.position, target.position + offset);
	}

	// Update is called once per frame
	void Update()
	{
		if(!target) return;

		actualDist = Vector3.Distance(transform.position, target.position + offset);

		transform.localScale = transform.parent.InverseTransformVector(new Vector3(transform.lossyScale.x, transform.lossyScale.y, actualDist / originalDist));
		transform.LookAt(target.position + offset);

    }

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		if (!Selection.Contains(this.gameObject)) return;

		Gizmos.DrawSphere(target.position + offset, 0.2f);
	}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(StrechTo))]
class StrechToEditor : Editor
{
	public override void OnInspectorGUI()
	{
		StrechTo s = target as StrechTo;

		base.DrawDefaultInspector();

		if (GUILayout.Button("Reset")) s.ResetDist();
	}
}
#endif