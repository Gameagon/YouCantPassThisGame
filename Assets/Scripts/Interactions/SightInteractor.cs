using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SightInteractor : Interactor
{
	public float sightDistance = 2;

	public LayerMask layerMask;

	private void FixedUpdate()
	{
		Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, sightDistance, layerMask);

		if(hit.collider)
		{

			if (!currentTarget || hit.collider.gameObject != currentTarget.gameObject)
			{
				InteractionTarget t = hit.collider.gameObject.GetComponent<InteractionTarget>();
				if (t == null) { Release(); }
				else { Select(t); }
			}
        }
		else
		{
			Release();
		}
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.color = currentTarget ? Color.red : Color.green;
		Gizmos.DrawLine(transform.position, transform.position + transform.forward * sightDistance);
	}
#endif
}
