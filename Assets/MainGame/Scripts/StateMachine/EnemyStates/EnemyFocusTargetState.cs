using System.Collections;
using UnityEngine;

public class EnemyFocusTargetState : EnemyState
{
    public EnemyFocusTargetState(Enemy owner) : base(owner)
    {

    }
    public override void Enter()
    {
        Owner.AnimationHandler.PlayAnimation(AnimationID.AIM_ON, AnimationLayer.AIM, false);
        Owner.AttackComponent.Enabled = true;
    }

    public override void Exit()
    {
        Owner.AnimationHandler.PlayAnimation(AnimationID.AIM_OFF, AnimationLayer.AIM, false);
        Owner.AttackComponent.Enabled = false;
    }

    public override void Update()
    {
        if (!Owner.IsInRangeWithCharacter())
        {
            Owner.StateMachine.ChangeState(Owner.IdleState);
        }
    }
}
