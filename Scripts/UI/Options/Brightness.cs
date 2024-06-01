using Godot;
using System;

public partial class Brightness : WorldEnvironment
{
	// Called when the node enters the scene tree for the first time.

	[Export]
	private string sensivilityKey; 
	public override void _EnterTree()
    {
        base._EnterTree();

        Environment.AdjustmentBrightness = OptionsSavesHandler.Current.GetValue(sensivilityKey)?.As<float>() ?? 1;
        OptionsSavesHandler.Current.onOptionsChanged += OnChangeBreightness;
    }
	public void OnChangeBreightness(string key, Variant value)
	{
		GD.Print(value);
		if(key == sensivilityKey)
			Environment.AdjustmentBrightness = value.As<float>();
	}
}
