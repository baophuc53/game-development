using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T>
{
    private ObjectPool<T> _instance;
    public ObjectPool<T> Instance { get => _instance; set => _instance = value; }

    private Dictionary<System.Type  , LinkedList<T>> _objects = new Dictionary<System.Type, LinkedList<T>>();

    //public T Get<T>(params object[] args)
    //{
    //    var listObject = _objects[typeof(T)];
    //    if (listObject == null)
    //    {
    //        listObject = new LinkedList<T>();
    //    }
    //}

    //private T Create<T>(params object[] args)
    //{
    //    var objectT = (T)System.Activator.CreateInstance(typeof(T), args);

    //    return objectT;
    //}

    //public void Push(obj)
    //{

    //}
}
