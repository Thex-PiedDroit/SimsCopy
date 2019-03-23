
using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class EventsHandler
{
#region Singleton

	static private EventsHandler m_instance = null;
	static private EventsHandler Instance
	{
		get
		{
			if (m_instance == null)
				m_instance = new EventsHandler();

			return m_instance;
		}
	}

	private EventsHandler()
	{
		m_listeners = new Dictionary<int, List<IEventsListener>>();
		m_events = new List<Enum>();
		m_queuedEventTriggers = new Queue<QueuedEvent>();
	}

	#endregion

#region Variables (private)

	private struct QueuedEvent
	{
		public Enum m_eventType;
		public object m_data;


		public QueuedEvent(Enum eventType, object data)
		{
			m_eventType = eventType;
			m_data = data;
		}
	}


	private Dictionary<int /*iEventID*/, List<IEventsListener>> m_listeners = null;
	private List<Enum> m_events = null;

	private Queue<QueuedEvent> m_queuedEventTriggers = null;

	private bool m_isFiringEvent = false;

	#endregion


	static public void Dispatch(Enum eventType, object data = null)
	{
		if (Instance.m_isFiringEvent)
		{
			Instance.EnqueueEvent(eventType, data);
			return;
		}

		Instance.Dispatch_Internal(eventType, data);
	}

	private void EnqueueEvent(Enum eventType, object data)
	{
		m_queuedEventTriggers.Enqueue(new QueuedEvent(eventType, data));
	}

	private void Dispatch_Internal(Enum eventType, object data)
	{
		List<IEventsListener> listeners = m_listeners[eventType.GetHashCode()];

		if (listeners == null)
			return;

		for (int i = 0, n = listeners.Count; i < n; ++i)
		{
			Assert.IsNotNull(listeners[i], string.Format("Someone is still listening to event \"{0}\" but is now null. Please make sure to unregister OnDestroy.", eventType.ToString()));

			listeners[i].HandleEvent(eventType, data);
		}

		if (m_queuedEventTriggers.Count > 0)
			DispatchNextQueuedEvent();
	}

	private void DispatchNextQueuedEvent()
	{
		QueuedEvent nextEvent = m_queuedEventTriggers.Dequeue();
		Dispatch_Internal(nextEvent.m_eventType, nextEvent.m_data);
	}

	static public void Register(IEventsListener listener, Enum[] events)
	{
		if (listener != null)
			Instance.Register_Internal(listener, events);
	}

	private void Register_Internal(IEventsListener listener, Enum[] events)
	{
		for (int i = 0; i < events.Length; ++i)
		{
			Enum eventType = events[i];

			int eventID = GetEventID(eventType);

			if (!m_listeners.ContainsKey(eventID))
			{
				m_events.Add(eventType);
				m_listeners[eventID] = new List<IEventsListener>();
			}
			else if (m_listeners[eventID].Contains(listener))
			{
				continue;
			}

			m_listeners[eventID].Add(listener);
		}
	}

	static public void Unregister(IEventsListener listener, params Enum[] events)
	{
		if (listener != null)
			Instance.Unregister_Internal(listener, events);
	}

	private void Unregister_Internal(IEventsListener listener, Enum[] events)
	{
		for (int i = 0; i < events.Length; ++i)
		{
			Enum eventType = events[i];

			int eventID = GetEventID(eventType);

			int listenerPlaceInList = m_listeners[eventID].Find(listener);

			if (listenerPlaceInList != -1)
			{
				m_listeners[eventID].RemoveSwapLast(listenerPlaceInList);
			}
		}
	}

	static private int GetEventID(Enum eventType)
	{
		return eventType.GetHashCode();
	}
}

public interface IEventsListener
{
	void HandleEvent(Enum eventType, object data);
}
