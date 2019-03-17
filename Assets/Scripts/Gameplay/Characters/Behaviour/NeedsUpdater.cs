
using System.Collections.Generic;
using UnityEngine;


public class NeedsUpdater
{
#region Variables (private)

	const float F_NEEDS_DECAY_IN_POINTS_PER_SECOND = 1.0f;


	private Dictionary<ENeedType, Need> m_pNeeds = null;

	private List<ENeedType> m_pLowNeedsSortedLowToHigh = null;

	#endregion


	public NeedsUpdater()
	{
		m_pNeeds = new Dictionary<ENeedType, Need>()
		{
			[ENeedType.BLADDER] = new Need(ENeedType.BLADDER),
			[ENeedType.FUN] = new Need(ENeedType.FUN),
			[ENeedType.HUNGER] = new Need(ENeedType.HUNGER),
			[ENeedType.SOCIAL] = new Need(ENeedType.SOCIAL),
			[ENeedType.ENERGY] = new Need(ENeedType.ENERGY),
			[ENeedType.HYGIENE] = new Need(ENeedType.HYGIENE),
		};

		m_pLowNeedsSortedLowToHigh = new List<ENeedType>();
	}

	public void UpdateNeeds()
	{
		foreach (KeyValuePair<ENeedType, Need> tNeed in m_pNeeds)
		{
			ENeedType eNeedType = tNeed.Key;

			DecayNeed(eNeedType);

			if (ShouldRegisterNeedAsLow(eNeedType))
				m_pLowNeedsSortedLowToHigh.Add(eNeedType);
		}
	}

	private bool ShouldRegisterNeedAsLow(ENeedType eNeedType)
	{
		bool bIsNeedLow = m_pNeeds[eNeedType].State != ENeedState.SATISFIED;

		return bIsNeedLow && !m_pLowNeedsSortedLowToHigh.Contains(eNeedType);
	}

	private void RegisterLowNeed(ENeedType eNeedType)
	{
		int iNewNeedPriority = NeedsToolkit.GetNeedPriority(eNeedType);

		int iNewIndex = 0;

		for (; iNewIndex < m_pLowNeedsSortedLowToHigh.Count; ++iNewIndex)
		{
			if (IsNeedLowerInPriority(m_pLowNeedsSortedLowToHigh[iNewIndex], iNewNeedPriority))
				break;
		}

		m_pLowNeedsSortedLowToHigh.Insert(iNewIndex, eNeedType);
	}

	private bool IsNeedLowerInPriority(ENeedType eNeedType, int iPriorityToCheckAgainst)
	{
		int iCurrentNeedPriority = NeedsToolkit.GetNeedPriority(eNeedType);

		return iCurrentNeedPriority < iPriorityToCheckAgainst;
	}

	private void DecayNeed(ENeedType eNeedType)
	{
		float fDecayThisFrame = F_NEEDS_DECAY_IN_POINTS_PER_SECOND * Time.deltaTime;

		m_pNeeds[eNeedType].Decay(fDecayThisFrame);
	}

	public NeedStateInfo GetNeedStateInfo(ENeedType eNeedType)
	{
		return m_pNeeds[eNeedType].GetStateInfo();
	}

	public ENeedType GetHighestPriorityNeed()
	{
		bool bHasAnyLowNeed = m_pLowNeedsSortedLowToHigh.Count > 0;

		return bHasAnyLowNeed ? GetHighestPriorityLowNeed() : GetLeastSatisfiedNeed();
	}

	private ENeedType GetHighestPriorityLowNeed()
	{
		return m_pLowNeedsSortedLowToHigh[0];
	}

	private ENeedType GetLeastSatisfiedNeed()
	{
		ENeedType eLowestNeedType = ENeedType.NONE;
		float fLowestSatisfaction = (float)ENeedState.SATISFIED;

		foreach (KeyValuePair<ENeedType, Need> tNeed in m_pNeeds)
		{
			ENeedType eCurrentNeedType = tNeed.Key;

			float fCurrentNeedSatisfaction = m_pNeeds[eCurrentNeedType].Satisfaction;

			if (fCurrentNeedSatisfaction < fLowestSatisfaction)
			{
				eLowestNeedType = eCurrentNeedType;
				fLowestSatisfaction = fCurrentNeedSatisfaction;
			}
		}

		return eLowestNeedType;
	}
}
