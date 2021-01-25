
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(CharacterController))]
public class Character : Unit
{
    #region Components
    public Gun Gun { get; private set; }
    #endregion

    protected override void InitComponents()
    {
        base.InitComponents();
        Gun = GetComponent<Gun>();
    }


    public void OnShootBegan()
    {
        Gun.Enabled = true;
        AnimationUtil.PlayAnimation(AnimationID.AIM_ON, AnimationLayer.AIM, false);

    }

    public void OnShootEnded()
    {
        Gun.Enabled = false;
        AnimationUtil.PlayAnimation(AnimationID.AIM_OFF, AnimationLayer.AIM, false);
    }

    public void PlayAnimation(string animationID)
    {
        var loop = true;

        //switch (animationID)
        //{
        //    case AnimationID.RUN:
        //        loop = true;
        //        AnimationUtil.SetFlip(Controller.velocity.x);
        //        break;

        //    case AnimationID.IDLE:
        //    case AnimationID.JUMP:
        //        loop = true;
        //        break;
        //    case AnimationID.DEAD:
        //    case AnimationID.CROUCH:
        //        loop = false;
        //        break;
        //}

        AnimationUtil.PlayAnimation(animationID, AnimationLayer.DEFAULT, loop);
    }

    protected override string GetOpponentBulletTag()
    {
        return ObjectTag.ENEMY_BULLET;
    }
}
