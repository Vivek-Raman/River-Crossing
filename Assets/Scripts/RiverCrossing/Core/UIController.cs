using System;
using dev.vivekraman.RiverCrossing.Core.Enums;
using dev.vivekraman.RiverCrossing.Core.States;
using UnityEngine;
using UnityEngine.Assertions;

namespace dev.vivekraman.RiverCrossing.Core
{
public class UIController : MonoBehaviour
{
  [SerializeField] private GameObject mainMenuPanel = null;

  private void Awake()
  {
    Assert.IsNotNull(mainMenuPanel);
  }

  public void SetMainMenuUIState(bool visible)
  {
    mainMenuPanel.SetActive(visible);
  }

  public void UI_PlayGame()
  {
    GameManager.Instance.StateManager.SetState(nameof(IdleState));
  }

  public void UI_SwitchGameModeToMissionariesAndCannibals()
  {
    SwitchGameMode(GameMode.MissionariesAndCannibals);
  }

  public void UI_SwitchGameModeToJealousHusbands()
  {
    SwitchGameMode(GameMode.JealousHusbands);
  }

  private void SwitchGameMode(GameMode gameMode)
  {
    GameManager.Instance.TheRuleEngine.TheGameMode = gameMode;
    GameManager.Instance.Spawner.LoadInitialStateForGameMode(gameMode);
  }
}
}
