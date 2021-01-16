using System.Collections;
using UnityEngine;

public class EnemyState
{
    public Enemy Owner { get; private set; }

    public EnemyState(Enemy owner)
    {
        this.Owner = owner;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
