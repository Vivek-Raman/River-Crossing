using System.Collections;

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

  public virtual IEnumerator OnStateEnter()
  {
    yield return null;
  }

  public virtual IEnumerator OnStateExit()
  {
    yield return null;
  }

  public virtual void OnStateTick()
  {
  }
}
}
