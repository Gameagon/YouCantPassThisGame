using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class Interactor : MonoBehaviour
{
	protected InteractionTarget currentTarget { get { return _currentTarget; } set 
		{
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();
			//if(value)Debug.Log(_currentTarget == value);
			if ((LockSelectionOnInteraction && interacting) || value == null || _currentTarget == value || !selectionFilter.HasFlag(value.group)) return;

			if (_currentTarget)
			{
				_currentTarget.Released(this);
			}

			_currentTarget = value;
			OnSelect.Invoke(this);

			_currentTarget.Selected(this);

			stopWatch.Stop();
			UnityEngine.Debug.Log(stopWatch.Elapsed);
		} }
	protected InteractionTarget _currentTarget = null;

	public InteractionTarget.Groups selectionFilter;

	public bool LockSelectionOnInteraction = false;

	//public List<InteractionTarget> forcedTargets = new List<InteractionTarget>();

	public UnityEvent<Interactor> OnSelect;
	public UnityEvent<Interactor> OnRelease;


	public bool interacting { get; private set; }

	public void Interact(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			Interact();

		}
		else if (context.canceled)
		{
			EndInteraction();
		}
	}

	public void Interact(InteractionTarget target = null)
	{
		if (target == null) currentTarget.Interact(this);
		else target.Interact(this);

		interacting = true;
	}

	public void EndInteraction(InteractionTarget target = null)
	{
		if (!interacting) return;

		if (target == null) currentTarget.InteractionEnd(this);
		else target.InteractionEnd(this);

		interacting = false;
	}

	public void Select(InteractionTarget target)
	{
		currentTarget = target;
	}

	public void Release()
	{
		if (LockSelectionOnInteraction && interacting) return;

		if (currentTarget)
		{
			UnityEngine.Debug.Log(currentTarget.gameObject);
			if (interacting)
			{
				EndInteraction();
			}

			OnRelease.Invoke(this);
			currentTarget.Released(this);
			_currentTarget = null;
		}
	}
}
