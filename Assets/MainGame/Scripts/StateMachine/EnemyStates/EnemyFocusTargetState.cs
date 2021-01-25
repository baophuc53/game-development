using System.Collections;
using UnityEngine;

public class EnemyFocusTargetState : UnitState
{
    public EnemyFocusTargetState(Enemy owner) : base(owner)
    {

    }
    public override void Enter()
    {
        Owner.AnimationUtil.PlayAnimation(AnimationID.AIM_ON, AnimationLayer.AIM, false);
        Owner.Gun.Enabled = true;
    }

    public override void Exit()
    {
        Owner.AnimationUtil.PlayAnimation(AnimationID.AIM_OFF, AnimationLayer.AIM, false);
        Owner.Gun.Enabled = false;
    }

    public override void Update()
    {
        if (!Owner.IsInRangeWithCharacter())
        {
            Owner.StateMachine.ChangeState(Owner.IdleState);
        }
    }
}
