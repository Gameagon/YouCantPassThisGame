using Godot;
using System;
using InputSystem;
using System.Collections.ObjectModel;
using Godot.NativeInterop;

public partial class Pala : AnimatableBody2D
{
	float Direction;
	[Export]
	float speed = 1.0f;

  [Export]
  ColorRect background;

  [Export] 
  ColorRect player;




float maxRight;
float maxLeft;
  public void Move(InputActionState state)
  {
    Direction = ((float)state.strength);
    maxRight = background.Position.X + background.Size.X - player.Size.X - 0.01f;
    maxLeft = background.Position.X;
    GD.Print("maxright" + maxRight);
    GD.Print("maxleft" + maxRight);

  }
  public override void _PhysicsProcess(double delta)
  {


    if (Direction == 1 && this.Position.X <= maxRight || Direction == -1 && this.Position.X >= maxLeft) 
    {
      this.Position = new Vector2(Direction * speed * (float)delta, 0) + Position;
    }



  }
}
