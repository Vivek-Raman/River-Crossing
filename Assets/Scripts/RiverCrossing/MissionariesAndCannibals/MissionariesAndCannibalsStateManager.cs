﻿using dev.vivekraman.RiverCrossing.MissionariesAndCannibals.Game.States;
using dev.vivekraman.RiverCrossing.StateManagement.Base;

namespace dev.vivekraman.RiverCrossing.MissionariesAndCannibals
{
// TODO: generalize using a custom rule engine if possible
public class MissionariesAndCannibalsStateManager : StateManager
{
  private void Awake()
  {
    base.RegisterState(new IdleState());
    base.RegisterState(new CharacterBoardingState());
    base.RegisterState(new BoatTraversingState());
    // base.RegisterState();
    // base.RegisterState();
  }

  private void Start()
  {
    base.SetState(nameof(IdleState));
  }
}
}
