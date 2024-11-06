﻿using dev.vivekraman.RiverCrossing.Utils.StateManagement;

namespace dev.vivekraman.RiverCrossing.Core.States
{
public class RiverCrossingStateManager : StateManager
{
  private void Awake()
  {
    base.RegisterState(new MainMenuState());
    base.RegisterState(new IdleState());
    base.RegisterState(new CharacterBoardingState());
    base.RegisterState(new BoatTraversingState());
    // base.RegisterState();
  }

  private void Start()
  {
    base.SetState(nameof(MainMenuState));
  }
}
}