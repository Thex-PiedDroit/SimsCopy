
using System;
using System.Collections.Generic;
using UnityEngine;


public class NeedsListView : MonoBehaviour, IEventsListener
{
#region Variables (serialized)

	[SerializeField]
	private Transform m_pListContainer = null;

	[SerializeField]
	private List<NeedDisplayItem> m_pNeedsDisplayItems = null;

	#endregion

#region Variables (private)

	private Character m_pCurrentCharacter = null;

	private Enum[] m_pEventsToListenTo = new Enum[] { EGenericGameEvents.CONTROLLED_CHARACTER_CHANGED };

	#endregion


	private void Awake()
	{
		InitializeNeedsDisplayItems();

		EventsHandler.Register(this, m_pEventsToListenTo);
	}

	private void OnDestroy()
	{
		EventsHandler.Unregister(this, m_pEventsToListenTo);
	}

	private void InitializeNeedsDisplayItems()
	{
		ENeedType[] pAllNeeds = Toolkit.GetEnumValues<ENeedType>();

		for (int i = 0; i < pAllNeeds.Length; ++i)
		{
			if (pAllNeeds[i] == ENeedType.NONE)
				continue;

			if (m_pNeedsDisplayItems.Count <= i)
				CreateNewNeedDisplayItem();

			InitializeNeedDisplayItem(m_pNeedsDisplayItems[i], pAllNeeds[i]);
		}
	}

	private void CreateNewNeedDisplayItem()
	{
		NeedDisplayItem pNewItem = Instantiate(m_pNeedsDisplayItems[0], m_pListContainer);
		m_pNeedsDisplayItems.Add(pNewItem);
	}

	private void InitializeNeedDisplayItem(NeedDisplayItem pItem, ENeedType eNeedType)
	{
		pItem.InitializeWithNeedType(eNeedType);
	}

	private void Update()
	{
		UpdateNeedsDisplay();
	}

	private void UpdateNeedsDisplay()
	{
		if (m_pCurrentCharacter == null)
			return;

		for (int i = 0; i < m_pNeedsDisplayItems.Count; ++i)
		{
			NeedDisplayItem pCurrentItem = m_pNeedsDisplayItems[i];

			Need pNeedData = GetNeedFromDisplayItem(pCurrentItem);
			pCurrentItem.UpdateGauge(pNeedData);
		}
	}

	private Need GetNeedFromDisplayItem(NeedDisplayItem pItem)
	{
		ENeedType eAssociatedNeed = pItem.GetAssociatedNeed();

		return m_pCurrentCharacter.GetNeedsUpdater().GetNeed(eAssociatedNeed);
	}

	public void HandleEvent(Enum eEventType, object pData)
	{
		if (!(eEventType is EGenericGameEvents))
			return;

		EGenericGameEvents eGameEvent = (EGenericGameEvents)eEventType;

		switch (eGameEvent)
		{
			case EGenericGameEvents.CONTROLLED_CHARACTER_CHANGED:
				{
					m_pCurrentCharacter = pData as Character;
					break;
				}
		}
	}
}
