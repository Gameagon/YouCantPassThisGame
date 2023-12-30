#if TOOLS
using Godot;
using InputSystem;
using System;
using System.Linq;
using System.Xml.Serialization;

[Tool]
public partial class SKInputSystem : EditorPlugin
{
	static private PackedScene imps = GD.Load<PackedScene>("res://addons/SKInputSystem/InputSelection/SKInputKMappingPanel.tscn");

	ListOfInputsInpector listOfInputsPlugin = new();

	public override void _EnterTree()
	{
		AddInspectorPlugin(listOfInputsPlugin);

		GD.Print("Inputs initialized");
	}

	public override void _ExitTree()
	{
		RemoveInspectorPlugin(listOfInputsPlugin);
	}

	static public void ShowInputMappingPanel(Action<Enum> action)
	{
		//GD.Print(((SceneTree)Engine.GetMainLoop()).Root.GetChild(0).GetChildren().FirstOrDefault(obj => obj.GetType() == typeof(SKInputSystem)));

		SKInputSelectionList popup = imps.Instantiate<SKInputSelectionList>();
		popup.CloseRequested += () => { popup.QueueFree(); };
		popup.confirmButton.Pressed += () =>
		{
			if(popup.key != null)
			{
				action.Invoke(popup.key);
				popup.QueueFree();
			}
		};
		//popup.DialogText = "Hello from the editor!";
		//GD.Print(3);
		EditorInterface.Singleton.PopupDialogCentered(popup);
	}
}
#endif
