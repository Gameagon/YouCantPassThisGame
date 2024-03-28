using Godot;
using System;

public static class SKMath
{
    public static Vector3 CalulateVelocity(this Vector3 currentVel, Vector3 direction, float acceleration,
        float maxSpeed = float.PositiveInfinity, float friction = 1, bool fly = false)
    {
        float trueAcceleration = friction * acceleration;
        if(trueAcceleration == 0) return currentVel;
        
        Vector3 normalidezDir = direction.Normalized();
        Vector3 desiredVel = new Vector3(normalidezDir.X * maxSpeed, fly ? normalidezDir.Y * maxSpeed : currentVel.Y, normalidezDir.Z * maxSpeed);

        return currentVel.Lerp(desiredVel, MathF.Min(trueAcceleration / currentVel.DistanceTo(desiredVel), 1));
    }

    public static float UnsignedMax(this float a, float b)
    {
        return MathF.Abs(a) > MathF.Abs(b) ? a : b;
    }

    public static float UnsignedMin(this float a, float b)
    {
        return MathF.Abs(a) < MathF.Abs(b) ? a : b;
    }
}
