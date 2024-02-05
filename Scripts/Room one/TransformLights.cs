using Godot;
using System;

using System.Collections.Generic;


public partial class TransformLights : Node3D
{
    [Export]
    public Node3D[] Lights;

    [Export]
    public Node3D Player;

    [Export]
    public float Radius;

    [Export]
    public float DistBetweenLights;

    [Export]
    public Vector3 Direction;

    [Export]
    public Vector3 FirstOffset;

    [Export]
    public Vector2 MinMax;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        for (int i = 0; i < Lights.Length; i++)
        {
            GD.Print("Start:" + Lights[i].GlobalPosition);
            Lights[i].GlobalPosition = this.GlobalPosition + FirstOffset + Direction.Normalized() * DistBetweenLights * i;
            GD.Print("End:" + Lights[i].GlobalPosition);
        }

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        for (int i = 0; i < Lights.Length; i++)
        {

            if (Player.GlobalPosition.DistanceTo(Lights[i].GlobalPosition) > Radius)
            {
                Vector3 endPos = Lights[i].GlobalPosition + (Direction.Normalized() * (Player.GlobalPosition - Lights[i].GlobalPosition)).Normalized() * DistBetweenLights * Lights.Length;
                Vector3 temp = endPos - (this.GlobalPosition + FirstOffset);
                float magnitude = MathF.Sqrt((float)Math.Pow(temp.X, 2) + (float)Math.Pow(temp.Y, 2) + (float)Math.Pow(temp.Z, 2));
                float dist = magnitude * Mathf.Sign(temp.Dot(Direction.Normalized()));
                GD.Print(Lights[i].GlobalPosition);

                if (dist < MinMax.Y && dist > MinMax.X && Player.GlobalPosition.DistanceTo(endPos) <= Radius)
                {
                    GD.Print(dist);
                    Lights[i].GlobalPosition = endPos;
                }
            }
        }
    }
}
        
    

