using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitEvent
{
    Attack,
    Move,
    Idle
}


[System.Serializable]
public class UnitController
{
    // Update is called once per frame
    //public void Tick()
    //{
    //    foreach(var component in Components)
    //    {
    //        if (component.Enabled)
    //        {
    //            component.Tick();
    //        }
    //    }
    //}

    //public T GetLogicComponent<T>() where T : BaseComponent
    //{
    //    return (T) Components.Find(item => item.GetType().Equals(typeof(T)));
    //}

    //public void NotifyEvent(UnitEvent unitEvent)
    //{

    //}
}
