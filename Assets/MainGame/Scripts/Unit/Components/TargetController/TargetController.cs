using Spine;
using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public abstract class TargetController : BaseComponent
{
    public SkeletonAnimation skeletonAnimation;

    [SpineBone(dataField: "skeletonAnimation")]
    public string targetBoneName;

    protected Bone targetBone;

    public override void InitComponents()
    {
        base.InitComponents();
        targetBone = skeletonAnimation.Skeleton.FindBone(targetBoneName);
    }

    // Update is called once per frame
    void Update()
    {
        HandleTargetPosition();
    }

    private void HandleTargetPosition()
    {
        var targetPosition = GetTargetPosition();
        var skeletonSpacePoint = skeletonAnimation.transform.InverseTransformPoint(targetPosition);
        skeletonSpacePoint.x *= skeletonAnimation.Skeleton.ScaleX;
        skeletonSpacePoint.y *= skeletonAnimation.Skeleton.ScaleY;
        targetBone.SetLocalPosition(skeletonSpacePoint);
    }

    protected abstract Vector3 GetTargetPosition();
}