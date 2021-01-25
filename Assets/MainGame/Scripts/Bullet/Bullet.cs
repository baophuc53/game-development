using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Normal,
    Lazer,
    Ice
}

public class Bullet : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    
    public BulletType bulletType = BulletType.Normal;

    public float Speed { get; set;}
    public int CarrayDamage { get; set; }

    private float flightiness = 100;
    private Vector2 direction = default(Vector2);

    private void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * Speed, Space.World);

        if (transform.position.magnitude > flightiness)
        {
            Finish();
        }
    }

    private void Finish()
    {
        PoolManager.ReleaseObject(this.gameObject);
    }

    public void Launch(Vector2 direction)
    {
        this.direction = direction;
    }
}
