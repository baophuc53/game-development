using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class ObjectPool : MonoBehaviour
//{
//    public static ObjectPool Instance;
//    public GameObject objectToPool;
//    public int amountToPool;

//    private LinkedList<GameObject> listObject;

//    private void Awake()
//    {
//        Instance = this;
//    }

//    private void Start()
//    {
//        listObject = new LinkedList<GameObject>();
//        for (int i = 0; i < amountToPool; i++)
//        {
//            GameObject obj = CreateObject();      
//            listObject.AddFirst(obj);
//        }
//    }

//    private GameObject CreateObject()
//    {
//        GameObject obj = (GameObject)Instantiate(objectToPool);
//        obj.SetActive(false);
//        return obj;
//    }

//    public GameObject GetObject()
//    {
//        print("Get OBjectttt");
//        GameObject obj = null;
//        if (listObject.Count > 0)
//        {
//            obj = (GameObject)listObject.First.Value;
//            listObject.RemoveFirst();
//        }

//        if (obj == null)
//        {
//            obj = CreateObject();
//            listObject.AddFirst(obj);
//        }


//        return obj;
//    }

//    public void PushObject(GameObject obj)
//    {
//        listObject.AddFirst(obj);
//    }
//}
