using Godot;
using InputSystem;
using System;

public partial class ShoutingNode : RigidBody3D
{
	[Export]
	public float Speed = 5;

	Vector3 MovementDir;

    public void Shout(InputActionState state)
    {
        //GD.Print(Name + " shouted " + state.state + " with strength " + state.strength);
    }

    public void Fly(InputActionState state)
    {
		Vector3 rawdir = ((Vector3)state.strength).Normalized();
        MovementDir.X = rawdir.X;
		MovementDir.Y = rawdir.Y;
		MovementDir.Z = -rawdir.Z;
        //GD.Print(Name + " shouted " + state.state + " with strength " + state.strength);
    }

    public override void _PhysicsProcess(double delta)
    {
        this.LinearVelocity = MovementDir * Speed;

        //GD.Print(LinearVelocity);
    }
}
