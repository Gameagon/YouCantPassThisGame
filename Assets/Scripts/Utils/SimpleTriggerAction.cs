using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleTriggerAction : MonoBehaviour
{
    public UnityEvent Enter;
    public UnityEvent Exit;
    public UnityEvent Stay;

    private void OnTriggerEnter(Collider other)
    {
        Enter.Invoke();
    }    
    
    private void OnTriggerExit(Collider other)
    {
        Exit.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        Stay.Invoke();
    }

}
