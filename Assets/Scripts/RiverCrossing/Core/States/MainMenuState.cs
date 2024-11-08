using System.Collections;
using dev.vivekraman.RiverCrossing.Utils.StateManagement;
using UnityEngine;

namespace dev.vivekraman.RiverCrossing.Core.States
{
public class MainMenuState : State
{
  public override string Name => nameof(MainMenuState);

  public override IEnumerator OnStateEnter()
  {
    GameManager gameManager = GameManager.Instance;
    gameManager.CanBoatMove = false;
    gameManager.CanBoardBoat = false;

    gameManager.TheUIController.SetMainMenuUIState(true);
    gameManager.TheCameraController.FocusMainMenu();
    yield break;
  }

  public override IEnumerator OnStateExit()
  {
    GameManager gameManager = GameManager.Instance;

    gameManager.TheUIController.SetMainMenuUIState(false);
    gameManager.TheCameraController.FocusGameplay();
    yield return new WaitForSeconds(1);
    yield break;
  }
}
}
