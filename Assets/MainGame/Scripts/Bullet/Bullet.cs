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
    public Rigidbody rigidBody;
    public BulletType bulletType = BulletType.Normal;
    public float Speed { get; set;}
    public float Flightiness { get; set;}
    private Vector2 direction = default(Vector2);
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * Speed, Space.World);

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
        this.direction = direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
