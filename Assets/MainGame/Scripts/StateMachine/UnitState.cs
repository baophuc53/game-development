

public abstract class UnitState
{
    public Unit Owner { get; private set; }

    public UnitState(Unit owner)
    {
        Owner = owner;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();

}
