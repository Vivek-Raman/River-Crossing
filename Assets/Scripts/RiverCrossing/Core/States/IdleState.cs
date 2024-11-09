using System.Collections;
using dev.vivekraman.RiverCrossing.Utils.StateManagement;

namespace dev.vivekraman.RiverCrossing.Core.States
{
public class IdleState : State
{
  public override string Name => nameof(IdleState);

  public override IEnumerator OnStateEnter()
  {
    GameManager gameManager = GameManager.Instance;
    gameManager.CanBoatMove = true;
    gameManager.CanBoardBoat = true;
    gameManager.TheRuleEngine.ShouldEvaluateRules = true;

    yield break;
  }
}
}
