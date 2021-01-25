using UnityEditor;
using UnityEngine;


public class Enemy : Unit
{
    #region Components
    [Header("Components")]
    public Gun Gun;
    #endregion

    public float maxRange = 2000f;

    public StateMachine StateMachine { get; private set; }
    public UnitState IdleState { get; private set; }
    public UnitState FocusTargetState { get; private set; }
    public UnitState DeadState { get; private set; }

    private void Start()
    {
        Gun.Enabled = false;
        IdleState = new EnemyIdleState(this);
        FocusTargetState = new EnemyFocusTargetState(this);
        DeadState = new EnemyDeadState(this);

        StateMachine = new StateMachine();
        StateMachine.ChangeState(IdleState);
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

    protected override string GetOpponentBulletTag()
    {
        return ObjectTag.CHARACTERE_BULLET;
    }


}