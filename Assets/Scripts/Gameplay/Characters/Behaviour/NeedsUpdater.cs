
using System.Collections.Generic;
using UnityEngine;


public class NeedsUpdater
{
#region Variables (private)

	const float NEEDS_DECAY_IN_POINTS_PER_SECOND = 1.0f;

	private readonly Dictionary<ENeedType, Need> m_needs = null;

	private List<ENeedType> m_lowNeedsSortedLowToHigh = null;

	#endregion


	public NeedsUpdater()
	{
		m_needs = new Dictionary<ENeedType, Need>()
		{
			[ENeedType.BLADDER] = new Need(ENeedType.BLADDER),
			[ENeedType.FUN] = new Need(ENeedType.FUN),
			[ENeedType.HUNGER] = new Need(ENeedType.HUNGER),
			[ENeedType.SOCIAL] = new Need(ENeedType.SOCIAL),
			[ENeedType.ENERGY] = new Need(ENeedType.ENERGY),
			[ENeedType.HYGIENE] = new Need(ENeedType.HYGIENE),
		};

		m_lowNeedsSortedLowToHigh = new List<ENeedType>();
	}

	public void UpdateNeeds()
	{
		foreach (KeyValuePair<ENeedType, Need> need in m_needs)
		{
			ENeedType needType = need.Key;

			DecayNeed(needType);

			if (ShouldRegisterNeedAsLow(needType))
				m_lowNeedsSortedLowToHigh.Add(needType);
		}
	}

	private bool ShouldRegisterNeedAsLow(ENeedType needType)
	{
		bool isNeedLow = m_needs[needType].State != ENeedState.SATISFIED;

		return isNeedLow && !m_lowNeedsSortedLowToHigh.Contains(needType);
	}

	private void RegisterLowNeed(ENeedType needType)
	{
		int newNeedPriority = NeedsToolkit.GetNeedPriority(needType);
		int newIndex = GetNewIndexInListBasedOnPriority(newNeedPriority);

		m_lowNeedsSortedLowToHigh.Insert(newIndex, needType);
	}

	private int GetNewIndexInListBasedOnPriority(int priority)
	{
		int index = 0;

		for (; index < m_lowNeedsSortedLowToHigh.Count; ++index)
		{
			if (IsNeedLowerInPriority(m_lowNeedsSortedLowToHigh[index], priority))
				break;
		}

		return index;
	}

	private bool IsNeedLowerInPriority(ENeedType needType, int priorityToCheckAgainst)
	{
		int currentNeedPriority = NeedsToolkit.GetNeedPriority(needType);

		return currentNeedPriority < priorityToCheckAgainst;
	}

	private void DecayNeed(ENeedType needType)
	{
		float decayThisFrame = NEEDS_DECAY_IN_POINTS_PER_SECOND * Time.deltaTime;

		m_needs[needType].Decay(decayThisFrame);
	}

	public NeedStateInfo GetNeedStateInfo(ENeedType needType)
	{
		return m_needs[needType].GetStateInfo();
	}

	public ENeedType GetHighestPriorityNeed()
	{
		bool hasAnyLowNeed = m_lowNeedsSortedLowToHigh.Count > 0;

		return hasAnyLowNeed ? GetHighestPriorityLowNeed() : GetLeastSatisfiedNeed();
	}

	private ENeedType GetHighestPriorityLowNeed()
	{
		return m_lowNeedsSortedLowToHigh[0];
	}

	private ENeedType GetLeastSatisfiedNeed()
	{
		ENeedType lowestNeedType = ENeedType.NONE;
		float lowestSatisfaction = (float)ENeedState.SATISFIED;

		foreach (KeyValuePair<ENeedType, Need> need in m_needs)
		{
			ENeedType currentNeedType = need.Key;

			float currentNeedSatisfaction = m_needs[currentNeedType].Satisfaction;

			if (currentNeedSatisfaction < lowestSatisfaction)
			{
				lowestNeedType = currentNeedType;
				lowestSatisfaction = currentNeedSatisfaction;
			}
		}

		return lowestNeedType;
	}
}
