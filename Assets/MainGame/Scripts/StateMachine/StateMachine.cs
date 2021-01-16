using System.Collections;
using UnityEngine;

public class StateMachine
{
    public EnemyState CurrentState { get; private set; }

    public void ChangeState(EnemyState state)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }

        CurrentState = state;
        CurrentState.Enter();
    }

    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }
}
