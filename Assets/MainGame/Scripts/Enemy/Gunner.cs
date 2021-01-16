using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Gunner : Enemy
{


    //public float shotSpeed = 3.0f;
    //public SkeletonAnimation skeletonAnimation;
    //public GameObject bullet;
    //public Vector2 lookDirection = new Vector2(-1, 0);
    //public int bulletSpeed = 500;
    //Rigidbody2D rigidbody2D;
    //float timer;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    rigidbody2D = GetComponent<Rigidbody2D>();
    //    timer = shotSpeed;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (timer > 0)
    //    {
    //        timer -= Time.deltaTime;
    //    } else
    //    {
    //        Attack();
    //        timer = shotSpeed;
    //    }
    //}

    //void Attack()
    //{
    //     skeletonAnimation.state.SetAnimation(1, "attack", false);
    //     skeletonAnimation.state.AddEmptyAnimation(1, 0.5f, 1f);
    //     GameObject projectileObject = Instantiate(bullet, rigidbody2D.position + Vector2.left * 0.5f, Quaternion.identity);
    //     EnemyBullet projectile = projectileObject.GetComponent<EnemyBullet>(); 

    //     projectile.Launch(lookDirection, bulletSpeed);
    //}
}
