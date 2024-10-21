namespace dev.vivekraman.RiverCrossing.StateManagement.Base
{
public abstract class State
{
  public abstract string Name { get; }

  public virtual bool CanEnterState()
  {
    return true;
  }

  public virtual bool CanExitState()
  {
    return true;
  }

  public virtual void OnStateEnter()
  {
  }

  public virtual void OnStateExit()
  {
  }

  public virtual void OnStateTick()
  {
  }
}
}
