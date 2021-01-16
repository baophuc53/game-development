using System.Collections;
using UnityEngine;
using Spine;
using Spine.Unity;

public class AttackComponent : Gun
{
    void Start()
    {
        Enabled = false;
    }

    private void FixedUpdate()
    {
        if (targetBone.X > 0)
        {
            skeletonAnimation.Skeleton.ScaleX = skeletonAnimation.Skeleton.ScaleX > 0 ? -1 : 1;
            return;
        }

        HandleAttack();
    }

    protected override void HandleFocusTarget()
    {
        var offset = new Vector3(0, 3, 0);
        var characterPosition = GameController.Instance.Character.transform.position + offset;
        var skeletonSpacePoint = skeletonAnimation.transform.InverseTransformPoint(characterPosition);
        skeletonSpacePoint.x *= skeletonAnimation.Skeleton.ScaleX;
        skeletonSpacePoint.y *= skeletonAnimation.Skeleton.ScaleY;
        targetBone.SetLocalPosition(skeletonSpacePoint);
    }
}