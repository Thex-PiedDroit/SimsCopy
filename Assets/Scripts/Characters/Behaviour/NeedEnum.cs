
/// <summary>
/// Sorted in order of priority (high number is higher priority).
/// </summary>
public enum ENeedType : int
{
	SOCIAL		= 0,
	FUN			= 1,
	ENERGY		= 2,
	HYGIENE		= 3,
	HUNGER		= 4,
	BLADDER		= 5,

	NONE		= 6
}

public enum ENeedState : int
{
	SATISFIED	= 100,
	LOW			= 50,
	CRITICAL	= 25,

	DEPLETED	= 0
}
