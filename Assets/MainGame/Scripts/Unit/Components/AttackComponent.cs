using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : BaseComponent
{
	public Bullet bullet;
	// Serial
	public SkeletonAnimation skeletonAnimation;
	[SpineBone(dataField: "skeletonAnimation")]
	public string aimTarget;
	[SpineBone(dataField: "skeletonAnimation")]
	public string aimRoot;

	public Camera Camera;

	[Header("Controls")]
	public string AttackButton = "Fire1";


	[Header("LogicData")]
	public float damage = 10;

	public Unit Target { get; set; }

	private Bone boneAimTarget;
	private Bone boneAimRoot;

	void Start()
	{
		boneAimTarget = skeletonAnimation.Skeleton.FindBone(aimTarget);
		boneAimRoot = skeletonAnimation.Skeleton.FindBone(aimRoot);
		
	}

    private void Update()
    {
		
	}

    public override void Tick()
	{
		if (Input.GetButton(AttackButton))
		{
			if (Attack())
            {
				Owner.NotifyEvent(UnitAction.Attack);
			}
		}
	}

	public Vector3 GetTargetPosition()
    {
		var mousePosition = Input.mousePosition;
		return Camera.ScreenToWorldPoint(mousePosition);
	}

	public bool Attack()
    {
		// Bullet --> Fire to mouse position

		var skeletonSpacePoint = skeletonAnimation.transform.InverseTransformPoint(GetTargetPosition());
		skeletonSpacePoint.x *= skeletonAnimation.Skeleton.ScaleX;
		skeletonSpacePoint.y *= skeletonAnimation.Skeleton.ScaleY;

		boneAimTarget.SetLocalPosition(skeletonSpacePoint);

		if (boneAimTarget.X < 0)
		{
			skeletonAnimation.Skeleton.ScaleX = skeletonAnimation.Skeleton.ScaleX > 0 ? -1 : 1;
		}

		var srcPosition = boneAimRoot.GetLocalPosition();
		var desPosition = boneAimTarget.GetLocalPosition();
		var detalPostion = desPosition - srcPosition;


		//bullet.transform.position = srcPosition;
		//bullet.Direction = desPosition;
		//bullet.transform.position = srcPosition;

		return true;
    }
}
