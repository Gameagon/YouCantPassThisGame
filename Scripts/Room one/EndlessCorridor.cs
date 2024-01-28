using Godot;
using System;

public partial class EndlessCorridor : Node3D
{
    [Export]
    private Node3D Target;
    [Export]
    public Vector3 offset;
    [Export]
    public Vector3 maxPosition;
    [Export]
    public Vector3 minPosition;
    [Export]
    public bool x, y, z;

    public Vector3 axis { get { return new Vector3(x ? 1 : 0, y ? 1 : 0, z ? 1 : 0); } }
    // Called when the node enters the scene tree for the first time.


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        this.GlobalPosition = (Target.GlobalPosition + offset).Clamp(minPosition, maxPosition) * axis;
    }
}
