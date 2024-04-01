using Godot;
using System;

public partial class RayInteractor3D : Interactor3D
{
	public override void _PhysicsProcess(double delta)
	{
        
        if((GodotObject)Call("get_collider") is Interactable3D _target)
		{
            if(Target != _target)
            {
                Select(_target);
            }
		}
        else if(Target != null) UnSelect();
    }
}
