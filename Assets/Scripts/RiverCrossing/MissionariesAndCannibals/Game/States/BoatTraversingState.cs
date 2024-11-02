using System.Collections;
using dev.vivekraman.RiverCrossing.Core;
using dev.vivekraman.RiverCrossing.StateManagement.Base;
using UnityEngine;

namespace dev.vivekraman.RiverCrossing.MissionariesAndCannibals.Game.States
{
public class BoatTraversingState : State
{
  public override string Name => nameof(BoatTraversingState);

  public override IEnumerator OnStateEnter()
  {
    GameManager gameManager = GameManager.Instance;
    gameManager.CanBoatMove = false;
    gameManager.CanBoardBoat = false;

    yield return new WaitForSeconds(1f);

    gameManager.StateManager.SetState(nameof(IdleState));
  }
}
}
