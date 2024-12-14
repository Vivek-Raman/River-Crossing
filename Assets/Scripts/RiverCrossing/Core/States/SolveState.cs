using System.Collections;
using dev.vivekraman.RiverCrossing.Core.Enums;
using dev.vivekraman.RiverCrossing.Utils.StateManagement;

namespace dev.vivekraman.RiverCrossing.Core.States
{
public class SolveState : State
{
  public override string Name => nameof(SolveState);

  public override IEnumerator OnStateEnter()
  {
    GameManager gameManager = GameManager.Instance;
    gameManager.CanBoatMove = false;
    gameManager.CanBoardBoat = false;
    gameManager.TheRuleEngine.ShouldEvaluateRules = false;
    gameManager.TheBoat.ForceAlightAll();

    yield break;
  }

  public override bool CanExitState()
  {
    return false;
  }
}
}
