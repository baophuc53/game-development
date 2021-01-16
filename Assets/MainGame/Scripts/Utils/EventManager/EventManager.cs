using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    private Dictionary<EventID, UnityAction<object>> listeners = new Dictionary<EventID, UnityAction<object>>();

    public void AddListener(EventID eventID, UnityAction<object> callback)
    {
        if (!listeners.ContainsKey(eventID))
        {
            listeners.Add(eventID, null);
            
        }
        listeners[eventID] += callback;
    }

    public void Dispatch(EventID eventID, object param = null)
    {
        if (!listeners.ContainsKey(eventID))
        {
            return;
        }

        var callback = listeners[eventID];
        if (callback != null)
        {
            callback(param);
        }
    }

    public void RemoveListener(EventID eventID, UnityAction<object> callback)
    {
        if (listeners.ContainsKey(eventID))
        {
            listeners[eventID] -= callback;
        }
    }

    public void ClearAllListener()
    {
        listeners.Clear();
    }
}
