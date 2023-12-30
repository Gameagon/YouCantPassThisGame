using Godot;
using InputSystem;
using System;

public partial class TestController : RigidBody3D
{
    [Export]
    public float Speed = 5;

    [Export]
    public float JumpForce = 10;

    Vector2 MovementDir;

    public void Movement(InputActionState state)
    {
        Vector2 rawdir = ((Vector2)state.strength).Normalized();
        MovementDir.X = rawdir.X;
        MovementDir.Y = rawdir.Y;

        //GD.Print(Name + " shouted " + state.state + " with strength " + state.strength);
    }
    public void Jump(InputActionState state)
    {
        if(state.state == InputActionState.PressState.JustPressed)
        {
            LinearVelocity = new Vector3(LinearVelocity.X, LinearVelocity.Y + JumpForce, LinearVelocity.Z);
        }

    }
    public override void _PhysicsProcess(double delta)
    {
        this.LinearVelocity = new Vector3(MovementDir.X * Speed, LinearVelocity.Y, -MovementDir.Y * Speed);

        //GD.Print(LinearVelocity);
    }
}
