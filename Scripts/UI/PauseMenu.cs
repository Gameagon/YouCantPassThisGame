using Godot;
using InputSystem;
using System;

public partial class PauseMenu : Control
{
    public override void _Ready()
    {
        base._Ready();

		Input.MouseMode = Input.MouseModeEnum.Visible;
    }

    public void Toggle(InputActionState state)
    {
        if (state.state == InputActionState.PressState.JustPressed)
		{
            if (Visible) Unpause();
            else Pause();
		}

    }

	public void Pause()
	{
        Show();
		GetTree().Paused = true;
		Input.MouseMode = Input.MouseModeEnum.Visible;
	}

	public void Unpause()
	{
		Hide();
		GetTree().Paused = false;
		Input.MouseMode = InputEventHandler.Current.MouseMode;
	}

	public void Quit()
	{
        GetTree().Quit();
    }
}