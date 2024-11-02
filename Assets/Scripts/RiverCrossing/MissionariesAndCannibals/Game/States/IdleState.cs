using System.Collections;
using dev.vivekraman.RiverCrossing.Core;
using dev.vivekraman.RiverCrossing.StateManagement.Base;

namespace dev.vivekraman.RiverCrossing.MissionariesAndCannibals.Game.States
{
public class IdleState : State
{
  public override string Name => nameof(IdleState);

  public override IEnumerator OnStateEnter()
  {
    GameManager.Instance.CanBoatMove = true;
    GameManager.Instance.CanBoardBoat = true;

    yield break;
  }
}
}
