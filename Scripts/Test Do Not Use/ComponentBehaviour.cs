using Godot;
using System;
using System.Diagnostics;
using System.Reflection;

[GlobalClass]
public partial class ComponentBehaviour : Resource
{
    public ComponentSystem node {get; private set;}
    public static float DeltaTime => ComponentSystem.DeltaTime;

    [Signal]
	public delegate void JumpEventHandler();

    //
    // Resumen:
    //     Change it at your own risk.
    public void SetNode(ComponentSystem _node)
    {
        node = _node;
    }

    [Export] public bool enabled{get; private set;} = true;

    public void Enable()
    {
        if(!enabled)
        {
            enabled = true;
            OnEnabled();
        }
    }

    public void Disable()
    {
        if(enabled)
        {
            enabled = false;
            OnDisabled();
        }
    }


    public virtual void OnEnabled(){}
    public virtual void OnDisabled(){}
    public virtual void Ready(){}
    public virtual void Process(){}
    public virtual void PhysicsProcess(){}
    public virtual void Input(InputEvent @event){}
    public virtual void ShortcutInput(InputEvent @event){}
}