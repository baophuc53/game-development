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
    public Rigidbody2D rigidBody2D;
    public BulletType bulletType = BulletType.Normal;
    public float Speed { get; set;}
    public float Flightiness { get; set;}

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    void Update()
    {
        if (transform.position.magnitude > Flightiness)
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
        var position = transform.position;
        rigidBody2D.AddForce(direction * Speed);
        //transform.Translate(direction * Time.deltaTime * speed, Space.World);
    }
}
