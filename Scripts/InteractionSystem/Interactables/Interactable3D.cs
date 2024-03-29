using Godot;
using System;

public partial class Interactable3D : Node3D
{
	[Signal]
	public delegate void SelectedEventHandler(Interactor3D interactor);
	[Signal]
	public delegate void UnSelectedEventHandler(Interactor3D interactor);
	[Signal]
	public delegate void ActivatedEventHandler(Interactor3D interactor);
	[Signal]
	public delegate void ReleasedEventHandler(Interactor3D interactor);

    public virtual void Select(Interactor3D _interactor)
	{
		EmitSignal(SignalName.Selected, _interactor);
	}

	public virtual void UnSelect(Interactor3D _interactor)
	{
		EmitSignal(SignalName.UnSelected, _interactor);
	}

	public virtual void Activate(Interactor3D _interactor)
	{
		EmitSignal(SignalName.Activated, _interactor);
    }

	public virtual void Release(Interactor3D _interactor)
	{
		EmitSignal(SignalName.Released, _interactor);
	}
}
