using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[Serializable]
public struct MinMaxVector
{
	public int min, max;

	public int size { get { return max - min; } }
}

[Serializable]
public struct MinMaxVector2D
{
	public MinMaxVector x, y;
	public int area { get { return x.size * y.size; } }
}

[Serializable]
public struct MinMaxVector3D
{
	public MinMaxVector x, y, z;

	public int volume { get { return x.size * y.size * z.size; } }
}

public static class SkullAGMaths

{
	
	static Vector3 TempVec3 = Vector3.zero;
	static Vector2 TempVec2 = Vector2.zero;

	///<summary>
	///	Is true if float a and b have the same, if one of the two is 0 will return false
	///	</summary>
	///	<param name="a">Float or int</param>
	///	<param name="b">Float or int</param>
	public static bool SignesAreEqual(float a, float b)
	{
		bool result = ((a < 0 && b < 0) || (a > 0 && b > 0));
		return result;
	}

	public static float MaxAbs(float a, float b)
	{
		float result = (Mathf.Abs(a) > Mathf.Abs(b)) ? a : b;
		return result;
	}

	///<summary>
	///	returns 1 or -1 if the given vaue is 0 returns 0
	///	</summary>
	///	<param name="a">Float or int</param>
	public static float ExtractSign(float a)
	{
		if (a == 0) return 0;
		float result = a / Mathf.Abs(a);
		return result;
	}

	///<summary>
	///	Interpolates the velocity usin acceleration and friction
	///	</summary>
	///	<param name="limitVelocity">The maximum velocity or the desired velocity, default is infinity</param>
	public static float CalculateVelocity(float currentVelocity, float acceleration, float limitVelocity = Mathf.Infinity, float friction = 1f)
	{
		return Mathf.Lerp(currentVelocity, limitVelocity, Mathf.Min((acceleration * friction * Time.deltaTime) / Mathf.Abs(currentVelocity - limitVelocity), 1));
	}

	///<summary>
	///	Interpolates the velocity usin acceleration and friction
	///	</summary>
	///	<param name="limitVelocity">The maximum velocity or the desired velocity, default is infinity</param>
	///	<param name="deceleration">If 0 acceleration will be used instead</param>
	public static Vector3 CalculateVelocity(Vector3 currentVelocity, Vector3 direction, float acceleration, float limitVelocity = Mathf.Infinity, float friction = 1f)
	{
		Vector3 limVel = direction * limitVelocity;

		return Vector3.Lerp(currentVelocity, limVel, Mathf.Min((acceleration * friction * Time.deltaTime) / Mathf.Abs(Vector3.Distance(currentVelocity, limVel)), 1));
	}

	///<summary>
	///	Interpolates the velocity usin acceleration and friction
	///	</summary>
	///	<param name="limitVelocity">The maximum velocity or the desired velocity, default is infinity</param>
	///	<param name="deceleration">If 0 acceleration will be used instead</param>
	public static Vector2 CalculateVelocity(Vector2 currentVelocity, Vector2 direction, float acceleration, float limitVelocity = 1000, float friction = 1f)
	{
		Vector2 limVel = direction * limitVelocity;

		return Vector2.Lerp(currentVelocity, limVel, Mathf.Min((acceleration * friction * Time.deltaTime) / Mathf.Abs(Vector2.Distance(currentVelocity, limVel)), 1));
	}


	///<summary>
	///	returns true if b is between a +- range
	///	default range is 0.001
	///	</summary>
	///	<param name="a">Float or int</param>
	///	<param name="b">Float or int</param>
	///	<param name="range">Float or int, is the radius of the circle range</param>
	public static bool Aproximately(float a, float b, float range = 0.001f)
	{
		bool result = ((a + range > b) && (a - range < b));
		return result;
	}

	public static bool AproximatelyVector2(Vector2 a, Vector2 b, float range = 0.001f)
	{
		bool resultX = ((a.x + range > b.x) && (a.x - range < b.x));
		bool resultY = ((a.y + range > b.y) && (a.y - range < b.y));
		return (resultX && resultY);
	}

	public static bool AproximatelyVector3(Vector3 a, Vector3 b, float range = 0.001f)
	{
		bool resultX = ((a.x + range > b.x) && (a.x - range < b.x));
		bool resultY = ((a.y + range > b.y) && (a.y - range < b.y));
		bool resultZ = ((a.z + range > b.z) && (a.z - range < b.z));
		return (resultX && resultY && resultZ);
	}

	public static float Hypotenuse(float legA, float legB)
	{
		float result = Mathf.Sqrt(Mathf.Pow(legA, 2) + Mathf.Pow(legB, 2));
		return result;
	}

	public static float Round(float a)
	{
		float diference = Mathf.Abs(a) % 1;
		float result = (diference > 0.5f) ? 1 - diference : -diference;
		result = a + result * ExtractSign(a);
		return result;
	}

	public static Vector2 RoundV2(Vector2 a)
	{
		Vector2 result = new Vector2(Round(a.x), Round(a.y));
		return result;
	}

	public static float CloseTo0(float a, float b)
	{
		return (Mathf.Abs(a) < Mathf.Abs(b)) ? a : b;
	}
	public static Vector2 CloseTo0(Vector2 a, Vector2 b)
	{
		return (a.magnitude < b.magnitude) ? a : b;
	}
	public static Vector3 CloseTo0(Vector3 a, Vector3 b)
	{
		return (a.magnitude < b.magnitude) ? a : b;
	}
	public static float FarFrom0(float a, float b)
	{
		return (Mathf.Abs(a) > Mathf.Abs(b)) ? a : b;
	}
	public static Vector2 FarFrom0(Vector2 a, Vector2 b)
	{
		return (a.magnitude > b.magnitude) ? a : b;
	}
	public static Vector3 FarFrom0(Vector3 a, Vector3 b)
	{
		return (a.magnitude > b.magnitude) ? a : b;
	}

	///<summary>
	///	returns the Vector2 rotated to the Dir vector this being Up
	///	</summary>
	public static Vector2 RotateVector(Vector2 vector, Vector2 dir)
	{
		dir = dir.normalized;
		TempVec2.x = vector.x * dir.y + vector.y * dir.x;
		TempVec2.y = vector.x * -dir.x + vector.y * dir.y;

		return TempVec2;
	}

	///<summary>
	///	returns the Vector2 rotated to the Dir vector this being Forward
	///	</summary>
	public static Vector3 RotateVector(Vector3 vector, Vector3 dir)
	{
		return Quaternion.LookRotation(dir) * vector;
	}

	public static Vector3 onePoleBezierLerp(Vector3 init, Vector3 end, Vector3 pole, float factor)
	{
		return Vector3.Lerp(Vector3.Lerp(init, pole, factor), Vector3.Lerp(pole, end, factor), factor);
	}

	public static Vector3 twoPoleBezierLerp(Vector3 init, Vector3 end, Vector3 pole1, Vector3 pole2, float factor)
	{
		Vector3 L1 = Vector3.Lerp(init, pole1, factor);
		Vector3 L2 = Vector3.Lerp(pole1, pole2, factor);
		Vector3 L3 = Vector3.Lerp(pole2, end, factor);

		return onePoleBezierLerp(L1, L3, L2, factor);
	}

	public static T GetCopyOf<T>(this T comp, T other) where T : Component
	{
		Type type = comp.GetType();
		Type otherType = other.GetType();
		if (type != otherType)
		{
			Debug.LogError($"The type \"{type.AssemblyQualifiedName}\" of \"{comp}\" does not match the type \"{otherType.AssemblyQualifiedName}\" of \"{other}\"!");
			return null;
		}

		BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default;
		PropertyInfo[] pinfos = type.GetProperties(flags);

		foreach (var pinfo in pinfos)
		{
			if (pinfo.CanWrite)
			{
				try
				{
					pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
				}
				catch
				{
				}
			}
		}

		FieldInfo[] finfos = type.GetFields(flags);

		foreach (var finfo in finfos)
		{
			finfo.SetValue(comp, finfo.GetValue(other));
		}
		return comp as T;
	}

	public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
	{
		return go.AddComponent<T>().GetCopyOf(toAdd) as T;
	}

	public static bool Probability(float probability, float maxProbability = 100)
	{
			return UnityEngine.Random.Range(0, maxProbability) <= probability;
	}

	public static bool Probability(int probability, int maxProbability = 100)
	{
		return UnityEngine.Random.Range(0, maxProbability) <= probability;
	}

	public static int ThrowDice(int faces)
	{
		return UnityEngine.Random.Range(0, faces);
	}

	public static Vector3 divide(this Vector3 a, Vector3 b)
	{
		return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
	}

	public static Vector3 invertX(this Vector3 vec)
	{
		vec.x = -vec.x;
		return vec;
	}
	public static Vector3 invertY(this Vector3 vec)
	{
		vec.y = -vec.y;
		return vec;
	}
	public static Vector3 invertZ(this Vector3 vec)
	{
		vec.z = -vec.z;
		return vec;
	}

	/*public static float Noise(Vector3 point)
	{
		Sim
	}*/

	//public static Vector3 invertY<Vector3>(this Vector3 vec) { return new Vector3(vec.x, -vec.y, vec.z); }
	//public static Vector3 invertZ<Vector3>(this Vector3 vec) { return new Vector3(vec.x, vec.y, -vec.z); }

	/*public static T OverlapCircleAll<T>(Vector2 point, float radius,int layerMask = Physics.DefaultRaycastLayers, float minDepth = -Mathf.Infinity, float maxDepth = Mathf.Infinity) where T : Collider2D
	{
		Collider2D[] collides = Physics2D.OverlapCircleAll(point, radius, layerMask, minDepth, maxDepth);
		for(int i = 0; i < collides.Length; i++)
		{
			Debug.Log(collides[i].GetType());
		}
		

		return null;
		//return colliders as T;
	}*/

}
