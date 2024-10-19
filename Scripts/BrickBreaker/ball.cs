using Godot;
using System;

public partial class ball : RigidBody2D
{
	[Export]
	float multiplyVel;
	
	
	[Export]
	float Vel = 0.1f;
	Vector2 Direction = Vector2.Zero;
	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{



	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		//this.ConstantForce = new Vector2(this.Position.X, this.Position.Y - Vel);
	}

	public void startBall()
	{



	}
}
