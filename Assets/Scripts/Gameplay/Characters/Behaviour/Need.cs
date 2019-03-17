
using UnityEngine;


public class Need
{
#region Variables (private)

	private readonly ENeedType m_eNeedType = ENeedType.NONE;

	public float Satisfaction { get; private set; } = 0.0f;
	public ENeedState State { get; private set; } = ENeedState.SATISFIED;

	private NeedStateInfo m_tStateInfo = new NeedStateInfo();

	#endregion


	public Need(ENeedType eNeedType)
	{
		m_eNeedType = eNeedType;
		Satisfaction = (float)ENeedState.SATISFIED;
	}

	public NeedStateInfo GetStateInfo()
	{
		m_tStateInfo.m_fSatisfaction = Satisfaction;
		m_tStateInfo.m_eState = State;

		return m_tStateInfo;
	}

	public void Decay(float fLoss)
	{
		int iPreviousValue = (int)Satisfaction;

		Satisfaction -= fLoss;

		if (iPreviousValue != (int)Satisfaction)
			ComputeNewState();

		if (State == ENeedState.DEPLETED)
			ClampToMin();
	}

	public void Replenish(float fGain)
	{
		int iPreviousValue = (int)Satisfaction;

		Satisfaction += fGain;

		if (iPreviousValue != (int)Satisfaction)
			ComputeNewState();

		if (State == ENeedState.SATISFIED)
			ClampToMax();
	}

	private void ComputeNewState()
	{
		ENeedState eNeedState = ENeedState.SATISFIED;

		if (IsValueInState(ENeedState.DEPLETED))
			eNeedState = ENeedState.DEPLETED;

		else if (IsValueInState(ENeedState.CRITICAL))
			eNeedState = ENeedState.CRITICAL;

		else if (IsValueInState(ENeedState.LOW))
			eNeedState = ENeedState.LOW;


		State = eNeedState;
	}

	private bool IsValueInState(ENeedState eNeedState)
	{
		return Satisfaction <= (float)eNeedState;
	}

	private void ClampToMin()
	{
		Satisfaction = Mathf.Max(NeedsToolkit.F_NEEDS_MIN_SATISFACTION, Satisfaction);
	}

	private void ClampToMax()
	{
		Satisfaction = Mathf.Min(Satisfaction, NeedsToolkit.F_NEEDS_MAX_SATISFACTION);
	}
}

public struct NeedStateInfo
{
	public float m_fSatisfaction;
	public ENeedState m_eState;
}
