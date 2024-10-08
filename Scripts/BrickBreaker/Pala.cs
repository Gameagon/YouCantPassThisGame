using Godot;
using System;
using InputSystem;

public partial class Pala : AnimatableBody2D
{
	float Direction;
	[Export]
	float speed = 1.0f;




	public void Move(InputActionState state)
    {

        Direction = ((float)state.strength);

    }
    public override void _PhysicsProcess(double delta)
    {
		this.Position = new Vector2(Direction * speed * (float)delta, 0) + Position;
    }
}
