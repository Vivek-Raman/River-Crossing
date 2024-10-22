using System.Collections;
using dev.vivekraman.RiverCrossing.Core;
using dev.vivekraman.RiverCrossing.StateManagement.Base;
using UnityEngine;

namespace dev.vivekraman.RiverCrossing.MissionariesAndCannibals.Game.States
{
public class CharacterBoardingState : State
{
  public override string Name => nameof(CharacterBoardingState);

  public override IEnumerator OnStateEnter()
  {
    GameManager gameManager = GameManager.Instance;
    gameManager.BoatCanMove = false;
    gameManager.CanBoardBoat = false;

    yield return new WaitForSeconds(1f);

    gameManager.StateManager.SetState(nameof(IdleState));
  }
}
}
