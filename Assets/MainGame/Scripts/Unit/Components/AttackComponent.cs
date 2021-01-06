using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : BaseComponent
{
	// Serial
	public SkeletonAnimation skeletonAnimation;
	[SpineBone(dataField: "skeletonAnimation")]
	public string boneName;
	public Camera cam;

	[Header("Controls")]
	public string AttackButton = "Fire1";


	[Header("LogicData")]
	public float damage = 10;

	public Unit Target { get; set; }

	private Bone bone;

	void Start()
	{
		bone = skeletonAnimation.Skeleton.FindBone(boneName);
	}

	public override void Tick()
	{
		bool attack = Input.GetButton(AttackButton);
		if (attack)
		{
			if (Attack())
            {
				Owner.NotifyEvent(UnitEvent.Attack);
			}
		}
	}

	public bool Attack()
    {
		if (Target == null)
        {
			return true;
        }

		return false;
    }

	public void Aim()
    {
		if (Target == null)
        {
			return;
        }

		var targetPosition = Target.transform.position;
		var worldPosition = cam.ScreenToWorldPoint(targetPosition);
		var skeletonSpacePoint = skeletonAnimation.transform.InverseTransformPoint(worldPosition);

		skeletonSpacePoint.x *= skeletonAnimation.Skeleton.ScaleX;
		skeletonSpacePoint.y *= skeletonAnimation.Skeleton.ScaleY;

        bone.SetLocalPosition(skeletonSpacePoint);

        if (bone.X < 0)
        {
            skeletonAnimation.Skeleton.ScaleX = skeletonAnimation.Skeleton.ScaleX > 0 ? -1 : 1;
        }
    }

	//void Update()
	//{
	//	var mousePosition = Input.mousePosition;
	//	var worldMousePosition = cam.ScreenToWorldPoint(mousePosition);
	//	var skeletonSpacePoint = skeletonAnimation.transform.InverseTransformPoint(worldMousePosition);
	//	skeletonSpacePoint.x *= skeletonAnimation.Skeleton.ScaleX;
	//	skeletonSpacePoint.y *= skeletonAnimation.Skeleton.ScaleY;

	//	//bone.SetLocalPosition(skeletonSpacePoint);

	//	//print($"One: {bone.X} - {skeletonAnimation.Skeleton.ScaleX}");
	//	//if (bone.X < 0)
	//	//{
	//	//	skeletonAnimation.Skeleton.ScaleX = skeletonAnimation.Skeleton.ScaleX > 0 ? -1 : 1;
	//	//	print($"Two: {bone.X} - {skeletonAnimation.Skeleton.ScaleX}");
	//	//}
	//}
}
