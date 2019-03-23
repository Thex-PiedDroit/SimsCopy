
static public class NeedsToolkit
{
	static public float NEEDS_MIN_SATISFACTION
	{
		get { return (float)ENeedState.DEPLETED; }
	}

	static public float NEEDS_MAX_SATISFACTION
	{
		get { return (float)ENeedState.SATISFIED; }
	}


	static public int GetNeedPriority(ENeedType need)
	{
		return (int)need;
	}
}
