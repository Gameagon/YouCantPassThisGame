using Godot;
using System;

public partial class OclussionCulling : Node
{
	// Called when the node enters the scene tree for the first time.
	public void OclussionCullingOn()
	{
		GetWindow().UseOcclusionCulling = true;
	}

	public void OclussionCullingOff()
	{
		GetWindow().UseOcclusionCulling = false;
	}
	// Called when the node enters the scene tree for the first time.

}
