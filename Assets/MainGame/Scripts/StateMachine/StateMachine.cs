using System.Collections;
using UnityEngine;

public class StateMachine
{
    public UnitState CurrentState { get; private set; }

    public void ChangeState(UnitState state)
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


public class CharacterStateMachine
{
    public UnitState CurrentState { get; private set; }

    public void ChangeState(UnitState state)
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
