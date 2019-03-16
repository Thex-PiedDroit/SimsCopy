
using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class EventsHandler
{
#region Singleton

	static private EventsHandler s_pInstance = null;

	static private EventsHandler Instance
	{
		get
		{
			if (s_pInstance == null)
				s_pInstance = new EventsHandler();

			return s_pInstance;
		}
	}

	private EventsHandler()
	{
		m_pListeners = new Dictionary<int, List<IEventsListener>>();
		m_pEvents = new List<Enum>();
		m_pQueuedEventTriggers = new Queue<QueuedEvent>();
	}

	#endregion

#region Variables (private)

	private struct QueuedEvent
	{
		public Enum m_eEventType;
		public object m_pData;


		public QueuedEvent(Enum eEventType, object pData)
		{
			m_eEventType = eEventType;
			m_pData = pData;
		}
	}


	private Dictionary<int /*iEventID*/, List<IEventsListener>> m_pListeners = null;
	private List<Enum> m_pEvents = null;

	private Queue<QueuedEvent> m_pQueuedEventTriggers = null;

	private bool m_bFiringEvent = false;

	#endregion


	static public void Dispatch(Enum eEvent, object pData = null)
	{
		if (Instance.m_bFiringEvent)
		{
			Instance.EnqueueEvent(eEvent, pData);
			return;
		}

		Instance.Dispatch_Internal(eEvent, pData);
	}

	private void EnqueueEvent(Enum eEvent, object pData)
	{
		m_pQueuedEventTriggers.Enqueue(new QueuedEvent(eEvent, pData));
	}

	private void Dispatch_Internal(Enum eEvent, object pData)
	{
		List<IEventsListener> pListeners = m_pListeners[eEvent.GetHashCode()];

		if (pListeners == null)
			return;

		for (int i = 0, n = pListeners.Count; i < n; ++i)
		{
			Assert.IsNotNull(pListeners[i], string.Format("Someone is still listening to event \"{0}\" but is now null. Please make sure to unregister OnDestroy.", eEvent.ToString()));

			pListeners[i].HandleEvent(eEvent, pData);
		}

		if (m_pQueuedEventTriggers.Count > 0)
			DispatchNextQueuedEvent();
	}

	private void DispatchNextQueuedEvent()
	{
		QueuedEvent tEvent = m_pQueuedEventTriggers.Dequeue();
		Dispatch_Internal(tEvent.m_eEventType, tEvent.m_pData);
	}

	static public void Register(IEventsListener pListener, Enum[] pEvents)
	{
		if (pListener != null)
			Instance.Register_Internal(pListener, pEvents);
	}

	private void Register_Internal(IEventsListener pListener, Enum[] pEvents)
	{
		for (int i = 0; i < pEvents.Length; ++i)
		{
			Enum eEventType = pEvents[i];

			int iEventID = GetEventID(eEventType);

			if (!m_pListeners.ContainsKey(iEventID))
			{
				m_pEvents.Add(eEventType);
				m_pListeners[iEventID] = new List<IEventsListener>();
			}
			else if (m_pListeners[iEventID].Contains(pListener))
			{
				continue;
			}

			m_pListeners[iEventID].Add(pListener);
		}
	}

	static public void Unregister(IEventsListener pListener, params Enum[] pEvents)
	{
		if (pListener != null)
			Instance.Unregister_Internal(pListener, pEvents);
	}

	private void Unregister_Internal(IEventsListener pListener, Enum[] pEvents)
	{
		for (int i = 0; i < pEvents.Length; ++i)
		{
			Enum eEventType = pEvents[i];

			int iEventID = GetEventID(eEventType);

			int iListenerPlaceInList = m_pListeners[iEventID].Find(pListener);

			if (iListenerPlaceInList != -1)
			{
				m_pListeners[iEventID].RemoveSwapLast(iListenerPlaceInList);
			}
		}
	}

	static private int GetEventID(Enum eEventType)
	{
		return eEventType.GetHashCode();
	}
}

public interface IEventsListener
{
	void HandleEvent(Enum eEventType, object pData);
}
