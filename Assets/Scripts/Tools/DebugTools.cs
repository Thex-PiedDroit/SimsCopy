
using UnityEngine;


static public class DebugTools
{
	static public void Log(string sMessage)
	{
		Debug.Log(sMessage);
	}

	static public void Log(string sMessage, params object[] pArgs)
	{
		sMessage = string.Format(sMessage, pArgs);
		Debug.Log(sMessage);
	}

	static public void LogWarning(string sMessage)
	{
		Debug.LogWarning(sMessage);
	}

	static public void LogWarning(string sMessage, params object[] pArgs)
	{
		sMessage = string.Format(sMessage, pArgs);
		Debug.LogWarning(sMessage);
	}

	static public void LogError(string sMessage)
	{
		Debug.LogError(sMessage);
	}

	static public void LogError(string sMessage, params object[] pArgs)
	{
		sMessage = string.Format(sMessage, pArgs);
		Debug.LogError(sMessage);
	}
}
