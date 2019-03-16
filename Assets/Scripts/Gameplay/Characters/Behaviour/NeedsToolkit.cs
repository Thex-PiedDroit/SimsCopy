
static public class NeedsToolkit
{
	static public float F_NEEDS_MIN_SATISFACTION
	{
		get { return (float)ENeedState.DEPLETED; }
	}

	static public float F_NEEDS_MAX_SATISFACTION
	{
		get { return (float)ENeedState.SATISFIED; }
	}


	static public int GetNeedPriority(ENeedType eNeed)
	{
		return (int)eNeed;
	}
}
