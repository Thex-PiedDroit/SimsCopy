
using System;
using UnityEngine;

static public class Toolkit
{
	static public T[] GetEnumValues<T>()
	{
		return (T[])Enum.GetValues(typeof(T));
	}
}
