using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InteractionTarget : MonoBehaviour
{
    [Flags]
    public enum Groups
    {
        None,g0,g1,g2,g3,g4,g5
    }

    [SingleEnumFlagSelect(EnumType = typeof(Groups))]
    public Groups group = Groups.g0;

    [SerializeField] private bool selfEndInteraction = true;

    public Interactor currentInteractor { get; private set; }

    //public Interactor LastSelector { get; private set; }

    public UnityEvent<Interactor> OnSelected;
    public UnityEvent<Interactor> OnReleased;
    public UnityEvent<Interactor> OnInteracted;
    public UnityEvent<Interactor> OnInteractionEnded;

    public void Selected(Interactor selector)
    {
        OnSelected.Invoke(selector);
    }

    public void Released(Interactor selector)
    {
        OnReleased.Invoke(selector);
    }

    public void Interact(Interactor interactor)
    {
        if(currentInteractor) return;

        currentInteractor = interactor;
        OnInteracted.Invoke(currentInteractor);
        if (selfEndInteraction) InteractionEnd(currentInteractor);
    }

    public void InteractionEnd(Interactor interactor)
    {
        if (currentInteractor != interactor) return;

        OnInteractionEnded.Invoke(currentInteractor);
        currentInteractor = null;
    }
}
