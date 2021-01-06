using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum UnitState
{
    Idle,
    Run,
    Jump,
    Crouch,
    Dead
}

public class Unit : MonoBehaviour
{
    [SerializeField]
    public List<BaseComponent> Components = new List<BaseComponent>();

    // Events
    public event UnityAction OnJump, OnLand, OnHardLand;

    public UnitState PreviousState { get; set; }
    private UnitState _state;
    public UnitState CurrentState { 
        get => _state; 
        set 
        {
            _state = value;
            if (_state != PreviousState) 
            {
                OnStateChanged(); 
            }
            PreviousState = _state;
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        Tick();
    }


    public void OnTakenDamage(float damage)
    {
        GetLogicComponent<HitPointComponent>().TakenDamge(damage);
    }

    public void Tick()
    {
        foreach (var component in Components)
        {
            if (component.Enabled)
            {
                component.Tick();
            }
        }
    }

    public T GetLogicComponent<T>() where T : BaseComponent
    {
        return (T)Components.Find(item => item.GetType().Equals(typeof(T)));
    }

    public void NotifyEvent(UnitEvent unitEvent)
    {

    }

    public void OnStateChanged()
    {
        var graphicComponent = GetLogicComponent<GraphicComponent>();
        var animation = "";

        switch (CurrentState)
        {
            case UnitState.Dead:
                
                break;

            case UnitState.Idle:
                animation = "idle";
                break;

            case UnitState.Crouch:
                animation = "crouch";
                break;

            case UnitState.Jump:
                animation = "rolling";
                break;

            case UnitState.Run:
                animation = "run";
                
                graphicComponent.SetFlip((int)GetLogicComponent<MoveComponent>().Direction);
                break;
        }
        graphicComponent.PlayAnimation(animation, (int)AnimationLayer.Default);
    }
}
