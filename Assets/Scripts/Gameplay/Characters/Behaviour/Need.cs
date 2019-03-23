
using UnityEngine;


public class Need
{
#region Variables (private)

	private readonly ENeedType m_needType = ENeedType.NONE;

	public float Satisfaction { get; private set; } = 0.0f;
	public ENeedState State { get; private set; } = ENeedState.SATISFIED;

	private NeedStateInfo m_stateInfo = new NeedStateInfo();

	#endregion


	public Need(ENeedType needType)
	{
		m_needType = needType;
		Satisfaction = (float)ENeedState.SATISFIED;
	}

	public NeedStateInfo GetStateInfo()
	{
		m_stateInfo.m_satisfaction = Satisfaction;
		m_stateInfo.m_state = State;

		return m_stateInfo;
	}

	public void Decay(float loss)
	{
		int iPreviousValue = (int)Satisfaction;

		Satisfaction -= loss;

		if (iPreviousValue != (int)Satisfaction)
			ComputeNewState();

		if (State == ENeedState.DEPLETED)
			ClampToMin();
	}

	public void Replenish(float gain)
	{
		int iPreviousValue = (int)Satisfaction;

		Satisfaction += gain;

		if (iPreviousValue != (int)Satisfaction)
			ComputeNewState();

		if (State == ENeedState.SATISFIED)
			ClampToMax();
	}

	private void ComputeNewState()
	{
		ENeedState needState = ENeedState.SATISFIED;

		if (IsValueInState(ENeedState.DEPLETED))
			needState = ENeedState.DEPLETED;

		else if (IsValueInState(ENeedState.CRITICAL))
			needState = ENeedState.CRITICAL;

		else if (IsValueInState(ENeedState.LOW))
			needState = ENeedState.LOW;


		State = needState;
	}

	private bool IsValueInState(ENeedState needState)
	{
		return Satisfaction <= (float)needState;
	}

	private void ClampToMin()
	{
		Satisfaction = Mathf.Max(NeedsToolkit.NEEDS_MIN_SATISFACTION, Satisfaction);
	}

	private void ClampToMax()
	{
		Satisfaction = Mathf.Min(Satisfaction, NeedsToolkit.NEEDS_MAX_SATISFACTION);
	}
}

public struct NeedStateInfo
{
	public float m_satisfaction;
	public ENeedState m_state;
}
