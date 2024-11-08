using System.Collections;
using dev.vivekraman.RiverCrossing.Utils.StateManagement;

namespace dev.vivekraman.RiverCrossing.Core.States
{
public class IdleState : State
{
  public override string Name => nameof(IdleState);

  public override IEnumerator OnStateEnter()
  {
    GameManager.Instance.CanBoatMove = true;
    GameManager.Instance.CanBoardBoat = true;
    GameManager.Instance.TheRuleEngine.ShouldEvaluateRules = true;

    yield break;
  }
}
}
