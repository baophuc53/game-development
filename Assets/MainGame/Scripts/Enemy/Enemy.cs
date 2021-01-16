using UnityEditor;
using UnityEngine;


public class Enemy : Unit
{
    #region Components
    [Header("Components")]
    public AttackComponent AttackComponent;
    #endregion

    public float maxRange = 2000f;

    public StateMachine StateMachine { get; private set; }
    public EnemyState IdleState { get; private set; }
    public EnemyState FocusTargetState { get; private set; }
    public EnemyState DeadState { get; private set; }

    private void Start()
    {
        IdleState = new EnemyIdleState(this);
        FocusTargetState = new EnemyFocusTargetState(this);
        DeadState = new EnemyDeadState(this);

        StateMachine = new StateMachine();
        StateMachine.ChangeState(IdleState);
        AttackComponent.Enabled = false;
    }

    private void Update()
    {
        StateMachine.Update();
    }

    public float DistanceToCharacter()
    {
        var characterPosition = GameController.Instance.Character.transform.position;
        var enemyPosition = gameObject.transform.position;

        var deltaX = characterPosition.x - enemyPosition.x;
        var deltaY = characterPosition.y - enemyPosition.y;
        var distance = deltaX * deltaX + deltaY * deltaY;
        return distance;
    }

    /// <summary>
    /// Character thuộc phạm vi phát hiện tấn công
    /// </summary>
    /// <returns></returns>
    public bool IsInRangeWithCharacter()
    {
        return DistanceToCharacter() <= maxRange;
    }

    //public float minDistanceToCharacter = 500f;

    //public EnemyState CurrentState { get; set; }
    //private void Start()
    //{
    //    AttackComponent = GetComponent<AttackComponent>();
    //    CurrentState = EnemyState.IDLE;
    //}

    //private void Update()
    //{
    //    HandleState();
    //}

    //public void SetState(EnemyState newState)
    //{
    //    var preState = CurrentState;
    //    CurrentState = newState;

    //    if (preState != newState)
    //    {
    //        OnStateChanged();
    //    }
    //}

    //private void HandleState()
    //{
    //    switch (CurrentState)
    //    {
    //        case EnemyState.IDLE:
    //            AttackComponent.Enabled = false;

    //            var characterPosition = GameController.Instance.Character.transform.position;
    //            var enemyPosition = gameObject.transform.position;

    //            var deltaX = characterPosition.x - enemyPosition.x;
    //            var deltaY = characterPosition.y - enemyPosition.y;
    //            var distance = deltaX * deltaX + deltaY * deltaY;
    //            if (distance <= minDistanceToCharacter)
    //            {
    //                SetState(EnemyState.FOCUS_TARGET);
    //            }
    //            break;
    //        case EnemyState.FOCUS_TARGET:
    //            AttackComponent.Enabled = true;
    //            break;

    //        case EnemyState.DEAD:
    //            AttackComponent.Enabled = false;

    //            break;
    //    }
    //}

    //public void OnStateChanged()
    //{
    //    switch (CurrentState)
    //    {
    //        case EnemyState.IDLE:
    //            AnimationHandler.PlayAnimation(AnimationID.IDLE, AnimationLayer.DEFAULT, true);

    //            break;

    //        case EnemyState.FOCUS_TARGET:
    //            AnimationHandler.PlayAnimation(AnimationID.AIM_ON, AnimationLayer.DEFAULT, false);
    //            break;

    //        case EnemyState.DEAD:
    //            AnimationHandler.PlayAnimation(AnimationID.DEAD, AnimationLayer.DEFAULT, false);
    //            break;
    //    }
    //}
}