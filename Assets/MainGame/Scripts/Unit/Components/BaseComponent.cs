using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseComponent : MonoBehaviour
{
    public Unit Owner;


    private bool _enabled = true;
    public bool Enabled { get => _enabled; set => _enabled = value; }

    public virtual void Tick()
    {
    }
}
