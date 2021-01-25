using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AnimationUtil))]
public abstract class Unit : MonoBehaviour
{
    #region Components
    public CharacterController Controller { get; private set; }
    public AnimationUtil AnimationUtil { get; private set; }
    #endregion


    #region Data

    [Header("Data")]
    public float JumpSpeed = 25;
    public float MoveSpeed = 7f;
    public float gravityScale = 6.6f;
    
    public int maxHitPoint = 10;

    private int currentHitPoint;
    #endregion

    private void Awake()
    {
        InitComponents();
    }

    protected virtual void InitComponents()
    {
        Controller = gameObject.GetComponent<CharacterController>();
        AnimationUtil = gameObject.GetComponent<AnimationUtil>();
        currentHitPoint = maxHitPoint;
    }

    public void OnTakenDamage(int damage)
    {
        currentHitPoint -= damage;
        if (currentHitPoint <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();

        if (other.CompareTag(GetOpponentBulletTag()))
        {
            OnTakenDamage(bullet.CarrayDamage);
        }
    }

    protected abstract string GetOpponentBulletTag();
}
