using Godot;
using Godot.Collections;
using MEC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

/*#if TOOLS
[Tool]
public partial class ComponentSystem : EditorPlugin
{
	public override void _EnterTree()
	{
		//EditorPlugin.AddCustomType("ComponentBehaviour", "ComponentBehaviour", ComponentBehaviour, );
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
	}
}
#endif*/


public partial class ComponentSystem : Node
{
	public static float DeltaTime = 0;

	[Export]
	public Array<ComponentBehaviour> components = new();

	[Signal]
	public delegate void JumpEventHandler();

    public override void _EnterTree()
    {
        base._EnterTree();

		foreach(ComponentBehaviour c in components)
		{
			c.SetNode(this);
		}
    }

    public override void _ExitTree()
    {
        base._ExitTree();

		foreach(ComponentBehaviour c in components)
		{
			c.SetNode(this);
		}
    }

	public override void _Ready()
	{
		base._Ready();
		//components.Add();
		foreach(ComponentBehaviour c in components)
		{
			c.Ready();
		}

		Debug.Print("Ready");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		DeltaTime = (float)delta;
		base._Process(delta);

		foreach(ComponentBehaviour c in components)
		{
			c.Process();
		}
	}

    public override void _PhysicsProcess(double delta)
    {
		DeltaTime = (float)delta;
        base._PhysicsProcess(delta);

		foreach(ComponentBehaviour c in components)
		{
			c.PhysicsProcess();
		}
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        //base._UnhandledInput(@event);
		
		foreach(ComponentBehaviour c in components)
		{
			c.Input(@event);
		}

		//GetViewport().SetInputAsHandled();
		@event.Dispose();
    }

    public override void _UnhandledKeyInput(InputEvent @event)
    {
        //base._UnhandledKeyInput(@event);

		foreach(ComponentBehaviour c in components)
		{
			c.ShortcutInput(@event);
		}
		
		if(@event.IsActionPressed("Jump"))
		{
			//Debug.Print("Jump");
			Vector3 a = new();
			a = a + a;

			EmitSignal("Jump"/*EventHandler*/);
			//JumpAction.Invoke();
		}

		//GetViewport().SetInputAsHandled();
		@event.Dispose();
    }
	
	public void JumpFunc()
	{
		Debug.Print("Jumped");
	}
}