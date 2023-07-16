using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TriggerInteractor : Interactor
{
    

    private void OnTriggerEnter(Collider other)
    {
        if (!currentTarget || other.gameObject != currentTarget.gameObject)
        {
            InteractionTarget t = other.gameObject.GetComponent<InteractionTarget>();
            if (t != null) { Select(t); }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == currentTarget.gameObject)
        {
            Release();
        }
    }
}
