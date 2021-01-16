using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class BaseComponent : MonoBehaviour
{
    public Unit Owner { get; private set; }

    public bool Enabled { get => enabled; set => enabled = value; }

    private void Awake()
    {
        InitComponents();
    }

    public virtual void InitComponents()
    {
        Owner = gameObject.GetComponent<Unit>();
    }
}
