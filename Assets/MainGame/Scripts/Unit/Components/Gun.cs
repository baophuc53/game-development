using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class Gun : BaseComponent
{
    public SkeletonAnimation skeletonAnimation;

    [SpineBone(dataField: "skeletonAnimation")]
    public string targetBoneName;
    [SpineBone(dataField: "skeletonAnimation")]
    public string bulletSpawnBoneName;
    [SpineBone(dataField: "skeletonAnimation")]
    public string aimRootBoneName;
    [SpineBone(dataField: "skeletonAnimation")]
    public string firePointBoneName;
    [Header("Prefab")]
    public GameObject bulletPrefab;

    [Header("Balance")]
    public float shootInterval = 0.25f;

    [Header("LogicData")]
    public float damage = 10;
    public float bulletSpeed = 500;
    public float bulletFlightiness = 200;

    public Camera Camera;

    private float lastShootTime;

    protected Bone targetBone;
    protected Bone bulletSpawnBone;
    protected Bone aimRootBone;
    protected Bone firePointBone;

    public override void InitComponents()
    {
        base.InitComponents();

        targetBone = skeletonAnimation.Skeleton.FindBone(targetBoneName);
        bulletSpawnBone = skeletonAnimation.Skeleton.FindBone(bulletSpawnBoneName);
        aimRootBone = skeletonAnimation.Skeleton.FindBone(aimRootBoneName);
        firePointBone = skeletonAnimation.Skeleton.FindBone(firePointBoneName);
    }

    private void Update()
    {
        HandleFocusTarget();
    }

    protected virtual void HandleFocusTarget()
    {
        var mousePosition = Input.mousePosition;
        var worldMousePosition = Camera.ScreenToWorldPoint(mousePosition);
        var skeletonSpacePoint = skeletonAnimation.transform.InverseTransformPoint(worldMousePosition);
        skeletonSpacePoint.x *= skeletonAnimation.Skeleton.ScaleX;
        skeletonSpacePoint.y *= skeletonAnimation.Skeleton.ScaleY;
        targetBone.SetLocalPosition(skeletonSpacePoint);
    }

    private void FixedUpdate()
    {
        if (targetBone.X < 0)
        {
            skeletonAnimation.Skeleton.ScaleX = skeletonAnimation.Skeleton.ScaleX > 0 ? -1 : 1;
            return;
        }
        
        HandleAttack();
    }

    protected virtual void HandleAttack()
    {
        var currentTime = Time.time;
        if (currentTime - lastShootTime > shootInterval)
        {
            Owner.AnimationHandler.PlayAnimation(AnimationID.SHOOT, AnimationLayer.SHOOT, false);

            lastShootTime = currentTime;

            var spawnPosition = firePointBone.GetWorldPosition(transform);

            var srcPosition = aimRootBone.GetWorldPosition(transform);
            var desPosition = targetBone.GetWorldPosition(transform);
            var direction = (desPosition - srcPosition).normalized;

            GameObject bulletObject = PoolManager.SpawnObject(bulletPrefab, spawnPosition, Quaternion.identity);
            if (bulletObject != null)
            {
                bulletObject.transform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * (Mathf.Atan2(direction.y, direction.x)));
                Bullet bullet = bulletObject.GetComponent<Bullet>();
                bullet.Speed = bulletSpeed;
                bullet.Flightiness = bulletFlightiness;

                bullet.Launch(direction);
            }
        }
    }
}
