
using System.Diagnostics;
using System.Text;


static public class DebugTools
{
	static public void LogError(string sMessage)
	{
		FinalizeLoggingError(sMessage);
	}

	static public void LogError(string sMessage, object pArg)
	{
		sMessage = FormatMessage(sMessage, pArg);

		FinalizeLoggingError(sMessage);
	}

	static public void LogError(string sMessage, params object[] pArgs)
	{
		sMessage = FormatMessage(sMessage, pArgs);

		FinalizeLoggingError(sMessage);
	}

	static private void FinalizeLoggingError(string sMessage)
	{
		sMessage = AttachCallerScriptName(sMessage);

		LogFormattedErrorMessage(sMessage);
	}

	static private string AttachCallerScriptName(string sMessage)
	{
		const int I_STACK_DISTANCE_FROM_CALLER = 3;

		StackTrace pStack = new StackTrace(I_STACK_DISTANCE_FROM_CALLER, false);

		string sScriptName = pStack.GetFrame(0).GetMethod().ReflectedType.ToString();

		StringBuilder sAppendedMessage = new StringBuilder("[");
		sAppendedMessage.Append(sScriptName);
		sAppendedMessage.Append("] - ");

		return sAppendedMessage.Append(sMessage).ToString();
	}

	static private void LogFormattedErrorMessage(string sMessage)
	{
		UnityEngine.Debug.LogError(sMessage);
	}


	static private string FormatMessage(string sMessage, object pArg)
	{
		return string.Format(sMessage, pArg);
	}

	static private string FormatMessage(string sMessage, params object[] pArgs)
	{
		return string.Format(sMessage, pArgs);
	}
}
