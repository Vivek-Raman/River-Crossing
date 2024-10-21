using dev.vivekraman.RiverCrossing.MissionariesAndCannibals.Game;
using dev.vivekraman.RiverCrossing.StateManagement.Base;

namespace dev.vivekraman.RiverCrossing.MissionariesAndCannibals
{
public class MissionariesAndCannibalsStateManager : StateManager
{
  private void Awake()
  {
    base.RegisterState(new IdleState());
    base.RegisterState(new CharacterBoardingState());
    // base.RegisterState();
    // base.RegisterState();
    // base.RegisterState();
    
    base.SetState(nameof(IdleState));
  }
}
}
