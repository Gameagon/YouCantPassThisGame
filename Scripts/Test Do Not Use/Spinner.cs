using Godot;
using System;

public partial class Spinner : Node3D
{
	[Export]
	public Script Speed;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		//Rotation += Vector3.Up * Speed * (float)delta;
    }
}
