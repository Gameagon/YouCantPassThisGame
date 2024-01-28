using Godot;



public partial class StrechTo : Node3D
{

	[Export]
	public Node3D target;

	[Export]
	public Godot.Vector3 offset;



	[Export]
	float originalDist;


	[Export]
	float actualDist;

	Vector3 OrigianlScale;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		ResetDist();
		//  GD.Print(actualDist,originalDist);
		OrigianlScale = this.Scale;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		actualDist = this.Position.DistanceTo(target.Position + offset);
		Vector3 globalScale = this.GlobalTransform.Basis.Scale;
		GD.Print(globalScale);
		this.Scale = OrigianlScale * (new Godot.Vector3(1, actualDist / originalDist, 1)); 

		//this.LookAt(target.Position + offset);
		//if (!target) return;



	}

	public static Godot.Vector3 inverseTransform(Godot.Vector3 globalDirection, Node3D target)
	{

		Transform3D global_to_local = target.GlobalTransform.AffineInverse();



		Godot.Vector3 Trans = global_to_local * globalDirection - global_to_local * Godot.Vector3.Zero;
		GD.Print(Trans);

		Trans = new Godot.Vector3(Trans.X, Trans.Y, Trans.Z);

		//Trans = new Godot.Vector3(Trans.X, -Trans.Y, -Trans.Z);
		//  Trans = InvertZ(Trans);

		return Trans;



	}

	public void ResetDist()
	{

		originalDist = this.Position.DistanceTo(target.Position + offset);

		/* thisnode.Position.X - target.Position.X + offset.X,
		 thisnode.Position.Y - target.Position.Y + offset.Y,
		 thisnode.Position.Z - target.Position.Y + offset.Z);



	   originalDist = Math.Sqrt(
						 Math.Pow(difference.X, 2f) +
						 Math.Pow(difference.Y, 2f) +
						 Math.Pow(difference.Z, 2f));*/
	}
	public static Godot.Vector3 InvertZ(Godot.Vector3 vectorOriginal)
	{
		Godot.Vector3 vectorInvertido = new Godot.Vector3(vectorOriginal.X, vectorOriginal.Y, -vectorOriginal.Z);
		return vectorInvertido;
	}


}
