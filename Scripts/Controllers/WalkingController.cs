using Godot;
using InputSystem;
using System;

public partial class WalkingController : CharacterBody3D
{
    [Export]
    public Camera3D camera;

    [ExportGroup("Movement")]
    [Export]
    public float Speed = 5.0f;

    [Export]
    public float Acceleration = 10f;

    [Export]
    public float JumpVelocity = 4.5f;

    public float RotationSensitivity = 1; 

    [Export(PropertyHint.Range, "0,1")]
    public float AirFrictioin = 0.1f;

    [Export]
    private string sensivilityKey;

    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    Vector2 MovementDir;

    Vector2 LookDelta;

    bool Jumping = false;

    Rect2 rect;


    PhysicsBody3D lastFloor;
    PhysicsMaterial floorMaterial = null;

    public override void _EnterTree()
    {
        base._EnterTree();

        RotationSensitivity = OptionsSavesHandler.Current.GetValue(sensivilityKey)?.As<float>() ?? 1;
        OptionsSavesHandler.Current.onOptionsChanged += SetSensivility;
        rect = GetViewport().GetVisibleRect();
    }

    public void SetSensivility(string key, Variant value)
    {
        if(key == sensivilityKey)
            RotationSensitivity = value.As<float>();
    }
    public void Move(InputActionState state)
    {
        MovementDir = ((Vector2)state.strength).Normalized();
    }

    public void Look(InputActionState state)
    {
        if (state.inputEvent is InputEventMouseMotion)
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
        Updatemovement(delta);
    }

    public override void _Process(double delta)
    {
        UpdateRotation(delta);
        MoveAndSlide();
    }


    void UpdateRotation(double delta)
    {
        camera.RotationDegrees = Vector3.Right * Math.Clamp(camera.RotationDegrees.X + LookDelta.Y * (float)delta, -90, 90);
        RotationDegrees -= Vector3.Up * LookDelta.X * (float)delta;
    }

    void Updatemovement(double delta)
    {
        Vector3 velocity = Velocity;

        PhysicsBody3D currentFloor = GetLastSlideCollision()?.GetCollider() as PhysicsBody3D;

        bool onFloor = IsOnFloor();

        if (!onFloor)
        {
            velocity.Y -= gravity * (float)delta;
            floorMaterial = null;
            lastFloor = null;
        }
        else
        {
            if (currentFloor != lastFloor)
            {
                floorMaterial = (PhysicsMaterial)currentFloor?.GetType().GetProperty("PhysicsMaterialOverride")?.GetValue(currentFloor);
            }

            lastFloor = currentFloor;

            if (Jumping)
            {
                velocity.Y = JumpVelocity;
            }
        }

        float friction = floorMaterial != null ? MathF.Max(floorMaterial.Friction, AirFrictioin) : AirFrictioin;

        

        Vector2 rotatedDir = MovementDir.Rotated(Rotation.Y);

        float maxVel = MathF.Max(friction > 1 ? Speed * friction : Speed, new Vector3(velocity.X, 0 , velocity.Y).Length());

        Velocity = velocity.CalulateVelocity(new Vector3(rotatedDir.X, 0, -rotatedDir.Y), Acceleration, maxVel, friction);
    }
}
