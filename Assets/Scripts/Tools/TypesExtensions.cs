
using UnityEngine;
using System;
using System.Collections.Generic;

static public class Vec2Extensions
{
#pragma warning disable IDE1006 // Naming Styles
	static public Vector3 xyz(this Vector2 tVec)
	{
		return tVec;	// Yep...
	}

	static public Vector3 xzy(this Vector2 tVec)
	{
		return new Vector3(tVec.x, 0.0f, tVec.y);
	}

	static public Vector2 x_(this Vector2 tVec)
	{
		return new Vector2(tVec.x, 0.0f);
	}

	static public Vector2 _y(this Vector2 tVec)
	{
		return new Vector2(0.0f, tVec.y);
	}

	static public Vector2 ShiftX(this Vector2 tVec, float fOffsetX)
	{
		tVec.x += fOffsetX;
		return tVec;
	}

	static public Vector2 ShiftY(this Vector2 tVec, float fOffsetY)
	{
		tVec.x += fOffsetY;
		return tVec;
	}

	static public Vector2 ShiftXY(this Vector2 tVec, Vector2 tOffset)
	{
		tVec += tOffset;
		return tVec;
	}

	static public Vector2 SetX(this Vector2 tVec, float fX)
	{
		tVec.x = fX;
		return tVec;
	}

	static public Vector2 SetY(this Vector2 tVec, float fY)
	{
		tVec.y = fY;
		return tVec;
	}

	static public Vector2 Clamp(this Vector2 tVec, Vector2 tMin, Vector2 tMax)
	{
		tVec.x = Mathf.Clamp(tVec.x, tMin.x, tMax.x);
		tVec.y = Mathf.Clamp(tVec.y, tMin.y, tMax.y);
		return tVec;
	}
}

static public class Vec3Extensions
{
	static public Vector2 xy(this Vector3 tVec)
	{
		return tVec;
	}

	static public Vector3 xzy(this Vector3 tVec)
	{
		return new Vector3(tVec.x, tVec.z, tVec.y);
	}

	static public Vector3 xyz(this Vector3 tVec)
	{
		return new Vector3(tVec.x, tVec.y, tVec.z);
	}

	static public Vector3 x_z(this Vector3 tVec)
	{
		return new Vector3(tVec.x, 0.0f, tVec.z);
	}

	static public Vector3 xy_(this Vector3 tVec)
	{
		return new Vector3(tVec.x, tVec.y);
	}

	static public Vector3 _yz(this Vector3 tVec)
	{
		return new Vector3(0.0f, tVec.y, tVec.z);
	}
#pragma warning restore IDE1006 // Naming Styles

	static public Vector3 ShiftX(this Vector3 tVec, float fOffsetX)
	{
		tVec.x += fOffsetX;
		return tVec;
	}

	static public Vector3 ShiftY(this Vector3 tVec, float fOffsetY)
	{
		tVec.x += fOffsetY;
		return tVec;
	}

	static public Vector3 ShiftZ(this Vector3 tVec, float fOffsetZ)
	{
		tVec.x += fOffsetZ;
		return tVec;
	}

	static public Vector3 ShiftXYZ(this Vector3 tVec, Vector3 tOffset)
	{
		tVec += tOffset;
		return tVec;
	}

	static public Vector3 SetX(this Vector3 tVec, float fX)
	{
		tVec.x = fX;
		return tVec;
	}

	static public Vector3 SetY(this Vector3 tVec, float fY)
	{
		tVec.y = fY;
		return tVec;
	}

	static public Vector3 SetZ(this Vector3 tVec, float fZ)
	{
		tVec.z = fZ;
		return tVec;
	}

	static public Vector3 Clamp(this Vector3 tVec, Vector3 tMin, Vector3 tMax)
	{
		tVec.x = Mathf.Clamp(tVec.x, tMin.x, tMax.x);
		tVec.y = Mathf.Clamp(tVec.y, tMin.y, tMax.y);
		tVec.z = Mathf.Clamp(tVec.z, tMin.z, tMax.z);
		return tVec;
	}
}

static public class ArraysExtensions
{
	static public T Last<T>(this T[] pArray)
	{
		return pArray[pArray.Length - 1];
	}

	static public bool Contains<T>(this T[] pArray, T pElement)
	{
		for (int i = 0, n = pArray.Length; i < n; ++i)
		{
			if (pArray[i].Equals(pElement))
				return true;
		}

		return false;
	}
}

static public class ListsExtensions
{
	static public List<T> Clone<T>(this List<T> pList)
	{
		List<T> tNewList = new List<T>(pList.Count);
		for (int i = 0; i < pList.Count; ++i)
			tNewList.Add(pList[i]);

		return tNewList;
	}

	/// <summary>
	/// Returns -1 if not found.
	/// </summary>
	static public int Find<T>(this List<T> pList, T pElement)
	{
		for (int i = 0, n = pList.Count; i < n; ++i)
		{
			if (pList[i].Equals(pElement))
				return i;
		}

		return -1;
	}

	static public T GetRandomElement<T>(this List<T> pList)
	{
		return pList[UnityEngine.Random.Range(0, pList.Count)];
	}

	static public void RemoveSwapLast<T>(this List<T> pList, int iIndex)
	{
		int iLastItemIndex = pList.Count - 1;

		pList[iIndex] = pList[iLastItemIndex];
		pList.RemoveAt(iLastItemIndex);
	}
}

static public class DictionariesExtensions
{
	static public Dictionary<T, U> Clone<T, U>(this Dictionary<T, U> pDictionary)
	{
		Dictionary<T, U> tNewDictionary = new Dictionary<T, U>(pDictionary.Count);
		foreach (KeyValuePair<T, U> pPair in pDictionary)
			tNewDictionary.Add(pPair.Key, pPair.Value);

		return tNewDictionary;
	}
}

static public class VariablesExtensions
{
	static public float Sqrd(this float fMe)
	{
		return fMe * fMe;
	}
}

static public class StringExtensions
{
	static public T ToEnum<T>(this string sMe, T eDefaultValue) where T : struct, IConvertible
	{
#if UNITY_EDITOR
		if (!typeof(T).IsEnum)
			throw new ArgumentException("T must be an enumerated type");
#endif

		if (string.IsNullOrEmpty(sMe))
			return eDefaultValue;

		foreach (T eValue in Enum.GetValues(typeof(T)))
		{
			if (eValue.ToString().ToLower().Replace("_", "").Equals(sMe.Trim().ToLower()))
				return eValue;
		}

#if UNITY_EDITOR
		Debug.LogWarning("Could not find matching enum for \"" + sMe + " in enum \"" + typeof(T).ToString() + "\"!");
#endif
		return eDefaultValue;
	}

	static public string Replace(this string sMe, int iIndex, char cChar)
	{
		return sMe.Remove(iIndex, 1).Insert(iIndex, cChar.ToString());
	}
}
