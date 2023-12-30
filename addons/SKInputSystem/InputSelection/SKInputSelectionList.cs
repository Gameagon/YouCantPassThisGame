using Godot;
using InputSystem;
using System;
using System.Collections.Generic;

[Tool]
public partial class SKInputSelectionList : Window
{
	// Called when the node enters the scene tree for the first time.
	[Export] public TextEdit textEdit;

	[Export] public Tree tree;

	[Export] public BaseButton confirmButton;

	public Enum key;

	Dictionary<TreeItem, Type> enumRelations = new();

	public override void _Ready()
	{
		textEdit.Editable = false;

		//GD.Print("a");
		tree.ItemSelected += OnItemSelected;

		TreeItem rootTT = tree.CreateItem();
		rootTT.SetText(0, "Root");
		
		TreeItem keyboardTT = tree.CreateItem(rootTT);
		keyboardTT.SetText(0, "Keyboard Keys");
		keyboardTT.Collapsed = true;
		
		enumRelations.Add(keyboardTT, typeof(Key));
		foreach(Key k in Enum.GetValues(typeof(Key)))
		{
			if(k > 0)
			{
				TreeItem temp = tree.CreateItem(keyboardTT);
				temp.SetText(0, k.ToString());
			}
		}

		TreeItem MouseTT = tree.CreateItem(rootTT);
		MouseTT.SetText(0, "Mose Buttons");
		MouseTT.Collapsed = true;
		
		enumRelations.Add(MouseTT, typeof(MouseButton));
		foreach(MouseButton k in Enum.GetValues(typeof(MouseButton)))
		{
			if(k > 0)
			{
				TreeItem temp = tree.CreateItem(MouseTT);
				temp.SetText(0, k.ToString());

				//Enum.Parse();
			}
			//temp.AddButton(0, null);
		}

		TreeItem JoypadTT = tree.CreateItem(rootTT);
		JoypadTT.SetText(0, "Joypad Buttons");
		JoypadTT.Collapsed = true;
		
		enumRelations.Add(JoypadTT, typeof(JoyButton));
		foreach(JoyButton k in Enum.GetValues(typeof(JoyButton)))
		{
			if(k >= 0)
			{
				TreeItem temp = tree.CreateItem(JoypadTT);
				temp.SetText(0, k.ToString());

				//Enum.Parse();
			}
			//temp.AddButton(0, null);
		}

		TreeItem JoypadAxisTT = tree.CreateItem(rootTT);
		JoypadAxisTT.SetText(0, "Joypad Axis");
		JoypadAxisTT.Collapsed = true;
		
		enumRelations.Add(JoypadAxisTT, typeof(JoyAxis));
		foreach(JoyAxis k in Enum.GetValues(typeof(JoyAxis)))
		{
			if(k >= 0)
			{
				TreeItem temp = tree.CreateItem(JoypadAxisTT);
				temp.SetText(0, k.ToString());

				//Enum.Parse();
			}
			//temp.AddButton(0, null);
		}

		TreeItem MidiTT = tree.CreateItem(rootTT);
		MidiTT.SetText(0, "Midi Message");
		MidiTT.Collapsed = true;
		
		enumRelations.Add(MidiTT, typeof(MidiMessage));
		foreach(MidiMessage k in Enum.GetValues(typeof(MidiMessage)))
		{
			if(k > 0)
			{
				TreeItem temp = tree.CreateItem(MidiTT);
				temp.SetText(0, k.ToString());

				//Enum.Parse();
			}
			//temp.AddButton(0, null);
		}
	}

    private void OnItemSelected()
    {
        if(tree.GetSelected().GetChildCount() == 0)
		{
			TreeItem selectedItem = tree.GetSelected();
			key = (Enum)Enum.Parse(enumRelations[selectedItem.GetParent()], selectedItem.GetText(0));

			textEdit.Text = InputEventHandler.ParseEnum(key);
		}
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

		if(textEdit.HasFocus() && @event.IsPressed()/*&& !mouse axis or something*/)
		{
			Enum ie = InputEventHandler.ParseInputEvent(@event);
			if (ie != null)
			{
				key = ie;
				textEdit.Text = InputEventHandler.ParseEnum(key);
				textEdit.ReleaseFocus();
			}
		}
    }
}
