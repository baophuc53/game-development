using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AnimationHandler))]
public class Unit : MonoBehaviour
{
    public CharacterController Controller { get; private set; }
    public AnimationHandler AnimationHandler { get; private set; }

    #region Data

    [Header("Data")]
    public float JumpSpeed = 25;
    public float MoveSpeed = 7f;
    public float gravityScale = 6.6f;

    #endregion

    private void Awake()
    {
        Controller = gameObject.GetComponent<CharacterController>();
        AnimationHandler = gameObject.GetComponent<AnimationHandler>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
    }
}
