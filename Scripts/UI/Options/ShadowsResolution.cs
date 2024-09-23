using Godot;
using System;

public partial class ShadowsResolution : Node
{
	// Called when the node enters the scene tree for the first time.
	public void ResolutionChange(Vector2I res)
	{
		GD.Print("AAAAAAAAAAAAAAAAAAAAAAAAAH");
		RenderingServer.DirectionalShadowAtlasSetSize(res[0], true);
		
		

	}
}
