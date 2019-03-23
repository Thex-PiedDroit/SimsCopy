
using System;
using UnityEngine;


[CreateAssetMenu(fileName = "EventObject", menuName = "EventObjects/DefaultEvent")]
public class EventObject : ScriptableObject
{
	public Action<EventData /*pData*/> OnEventFired = null;


	public void FireEvent(EventData data)
	{
		OnEventFired?.Invoke(data);
	}
}

abstract public class EventData
{

}
