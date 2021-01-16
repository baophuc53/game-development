
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CharacterState
{
    IDLE,
    MOVING,
    JUMP,
    CROUCH,
    DEAD
}

[RequireComponent(typeof(CharacterController))]
public class Character : Unit
{
    #region Components
    
    [Header("Components")]
    public Gun Gun;

    #endregion

    [Header("Prefab")]
    public GameObject bulletPrefab;

    #region Events
    public UnityAction Shoot;
    public UnityAction Run;
    public UnityAction Jump;

    #endregion

    public CharacterState CurrentState { get; private set; }


    private void Start()
    {
        Gun.Enabled = false;
    }

    private void Update()
    {
        
    }

    public void OnShootBegin()
    {
        Gun.Enabled = true;
        AnimationHandler.PlayAnimation(AnimationID.AIM_ON, AnimationLayer.AIM, false);
    }

    public void OnShootFinish()
    {
        Gun.Enabled = false;
        AnimationHandler.PlayAnimation(AnimationID.AIM_OFF, AnimationLayer.AIM, false);
    }

    public void SetState(CharacterState newState)
    {
        var preState = CurrentState;
        CurrentState = newState;

        if (preState != newState)
        {
            OnStateChanged();
        }
    }

    public void OnStateChanged()
    {
        switch (CurrentState)
        {
            case CharacterState.IDLE:
                AnimationHandler.PlayAnimation(AnimationID.IDLE, AnimationLayer.DEFAULT, true);
                break;
            case CharacterState.MOVING:
                AnimationHandler.SetFlip(Controller.velocity.x);
                AnimationHandler.PlayAnimation(AnimationID.RUN, AnimationLayer.DEFAULT, true);
                break;
            case CharacterState.JUMP:
                AnimationHandler.PlayAnimation(AnimationID.JUMP, AnimationLayer.DEFAULT, true);
                break;
            case CharacterState.CROUCH:
                AnimationHandler.PlayAnimation(AnimationID.CROUCH, AnimationLayer.DEFAULT, true);
                break;
            case CharacterState.DEAD:
                AnimationHandler.PlayAnimation(AnimationID.DEAD, AnimationLayer.DEFAULT, true);
                break;
        }
    }
}
