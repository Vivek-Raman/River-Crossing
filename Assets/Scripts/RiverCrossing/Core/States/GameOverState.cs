using System.Collections;
using dev.vivekraman.RiverCrossing.Utils.StateManagement;

namespace dev.vivekraman.RiverCrossing.Core.States
{
public class GameOverState : State
{
  public override string Name => nameof(GameOverState);

  public override IEnumerator OnStateEnter()
  {
    GameManager gameManager = GameManager.Instance;
    gameManager.CanBoatMove = false;
    gameManager.CanBoardBoat = false;
    gameManager.TheRuleEngine.ShouldEvaluateRules = false;

    gameManager.TheUIController.ShowGameOverPanel();
    yield break;
  }

  public override bool CanExitState()
  {
    return false;
  }
}
}
