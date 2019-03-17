
using System;
using System.Collections.Generic;
using UnityEngine;


public class NeedsListView : MonoBehaviour, IEventsListener
{
#region Variables (serialized)

	[SerializeField]
	private Transform m_listContainer = null;

	[SerializeField]
	private List<NeedDisplayItem> m_needsDisplayItems = null;

	#endregion

#region Variables (private)

	static private readonly Enum[] EVENTS_TO_LISTEN_TO = new Enum[] { EGenericGameEvents.CONTROLLED_CHARACTER_CHANGED };

	private Character m_currentCharacter = null;

	#endregion


	private void Awake()
	{
		InitializeNeedsDisplayItems();

		EventsHandler.Register(this, EVENTS_TO_LISTEN_TO);
	}

	private void OnDestroy()
	{
		EventsHandler.Unregister(this, EVENTS_TO_LISTEN_TO);
	}

	private void InitializeNeedsDisplayItems()
	{
		ENeedType[] allNeeds = Toolkit.GetEnumValues<ENeedType>();

		for (int i = 0; i < allNeeds.Length; ++i)
		{
			ENeedType needType = allNeeds[i];

			if (needType == ENeedType.NONE)
				continue;

			if (m_needsDisplayItems.Count <= i)
				CreateNewNeedDisplayItem();

			m_needsDisplayItems[i].InitializeWithNeedType(needType);
		}
	}

	private void CreateNewNeedDisplayItem()
	{
		NeedDisplayItem newItem = Instantiate(m_needsDisplayItems[0], m_listContainer);
		m_needsDisplayItems.Add(newItem);
	}

	private void Update()
	{
		UpdateNeedsDisplay();
	}

	private void UpdateNeedsDisplay()
	{
		if (m_currentCharacter == null)
			return;

		for (int i = 0; i < m_needsDisplayItems.Count; ++i)
		{
			NeedDisplayItem currentItem = m_needsDisplayItems[i];

			NeedStateInfo needStateInfo = GetNeedStateInfoFromDisplayItem(currentItem);
			currentItem.UpdateGauge(needStateInfo);
		}
	}

	private NeedStateInfo GetNeedStateInfoFromDisplayItem(NeedDisplayItem item)
	{
		ENeedType associatedNeed = item.GetAssociatedNeed();

		return m_currentCharacter.GetNeedsUpdater().GetNeedStateInfo(associatedNeed);
	}

	public void HandleEvent(Enum eventType, object data)
	{
		if (!(eventType is EGenericGameEvents))
			return;

		EGenericGameEvents gameEvent = (EGenericGameEvents)eventType;

		switch (gameEvent)
		{
			case EGenericGameEvents.CONTROLLED_CHARACTER_CHANGED:
				{
					m_currentCharacter = data as Character;
					break;
				}
		}
	}
}
