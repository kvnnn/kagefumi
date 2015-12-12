using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class CustomVector
{
	public static Vector2 MultiplyX(this Vector2 vector, int x)
	{
		vector.x = vector.x * x;
		return vector;
	}

	public static Vector2 ConvertStringToVector2(string str, char splitChar = ',')
	{
		string[] strArray = str.Split(splitChar);
		if (strArray.Length <= 1)
		{
			Debug.LogError("Cant convert string to Vector2");
			return Vector2.zero;
		}

		return new Vector2(float.Parse(strArray[0]), float.Parse(strArray[1]));
	}

	public static Vector3 ConvertStringToVector3(string str, char splitChar = ',')
	{
		string[] strArray = str.Split(splitChar);
		if (strArray.Length <= 2)
		{
			Debug.LogError("Cant convert string to Vector3");
			return Vector3.zero;
		}

		return new Vector3(float.Parse(strArray[0]), float.Parse(strArray[1]), float.Parse(strArray[2]));
	}
}
