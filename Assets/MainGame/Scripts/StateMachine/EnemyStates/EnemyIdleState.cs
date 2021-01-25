using System.Collections;
using UnityEngine;


public class EnemyIdleState : UnitState
{
    public EnemyIdleState(Enemy owner) : base(owner)
    {
    }

    public override void Enter()
    {
        Owner.AnimationUtil.PlayAnimation(AnimationID.IDLE, AnimationLayer.DEFAULT, true);
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        if (Owner.IsInRangeWithCharacter())
        {
            Owner.StateMachine.ChangeState(Owner.FocusTargetState);
        }
    }
}
