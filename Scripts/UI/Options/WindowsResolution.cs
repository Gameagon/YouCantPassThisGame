using Godot;
using System;

public partial class WindowsResolution : Node
{
	// Called when the node enters the scene tree for the first time.
	public void ResolutionChange(Vector2I res)
	{
		DisplayServer.WindowSetSize(res,0);
		GetWindow().ContentScaleSize = res;
		
	}
}
