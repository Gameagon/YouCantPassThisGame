using Godot;
using InputSystem;
using System;

public partial class WalkingController : CharacterBody3D
{
    [Export]
    public float Speed = 5.0f;

    [Export]
    public float JumpVelocity = 4.5f;

    [Export]
    public Camera3D camera;

    [Export]
    public float RotationSensitivity = 1;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    Vector2 MovementDir;

    Vector2 LookDelta;

    bool Jumping = false;

    Rect2 rect;

    public override void _EnterTree()
    {
        base._EnterTree();

        rect = GetViewport().GetVisibleRect();
    }

    public void Move(InputActionState state)
    {
        MovementDir = ((Vector2)state.strength).Normalized();
    }

	public void Look(InputActionState state)
	{
        if(state.inputEvent is InputEventMouseMotion)
        {
            LookDelta = Vector2.Zero;
            Vector2 l = (Vector2)state.strength * RotationSensitivity;

            camera.RotationDegrees = Vector3.Right * Math.Clamp(camera.RotationDegrees.X - l.Y, -90, 90);
            RotationDegrees -= Vector3.Up * l.X;
        }
        else
        {
            LookDelta = (Vector2)state.strength * RotationSensitivity * 360;
        }
    }

    public void Jump(InputActionState state)
    {
        if (state.state == InputActionState.PressState.JustPressed)
        {
            Jumping = true;
        }
        else if (state.state == InputActionState.PressState.Released)
        {
            Jumping = false;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        UpdateRotation(delta);
        Updatemovement(delta);
    }
    
    void UpdateRotation(double delta)
    {
        camera.RotationDegrees = Vector3.Right * Math.Clamp(camera.RotationDegrees.X + LookDelta.Y * (float)delta, -90, 90);
        RotationDegrees -= Vector3.Up * LookDelta.X * (float)delta;
    }

    void Updatemovement(double delta)
    {
        Vector3 velocity = Velocity;

        if (!IsOnFloor())
            velocity.Y -= gravity * (float)delta;
        else if (Jumping)
            velocity.Y = JumpVelocity;

        Vector2 rotyatedDir = MovementDir.Rotated(Rotation.Y);
        velocity.X = rotyatedDir.X * Speed;
        velocity.Z = -rotyatedDir.Y * Speed;

        Velocity = velocity;
        MoveAndSlide();
    }
}
