using Godot;
using System;

public partial class WindowMode : CheckBox
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public string VolumeKey;

	[Export]
	public int SetScreen;
	[Export]
	public int SetScreenOff;
	public override void _EnterTree()
	{
		base._EnterTree();
		bool isOn = OptionsSavesHandler.Current.GetValue(VolumeKey)?.As<bool>() ?? true;
		SetWindow(isOn);

	}
	
	public void Onclick()
	{
		SetWindow(this.ButtonPressed);
	}
	private void SetWindow(bool isOn)
	{
		this.ButtonPressed = isOn;
		if(isOn == true)
		{
			DisplayServer.WindowSetMode((DisplayServer.WindowMode)SetScreen,0);
		}
		else
		{
			DisplayServer.WindowSetMode((DisplayServer.WindowMode)SetScreenOff,0);

		}
		OptionsSavesHandler.Current.SetValue(VolumeKey, this.ButtonPressed);
		
	}
}





