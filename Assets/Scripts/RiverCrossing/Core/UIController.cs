using dev.vivekraman.RiverCrossing.Core.Enums;
using dev.vivekraman.RiverCrossing.Core.States;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace dev.vivekraman.RiverCrossing.Core
{
public class UIController : MonoBehaviour
{
  [SerializeField] private GameObject mainMenuPanel = null;
  [SerializeField] private GameObject gameplayHudPanel = null;

  private void Awake()
  {
    Assert.IsNotNull(mainMenuPanel);
    Assert.IsNotNull(gameplayHudPanel);
  }

  private void Start()
  {
    UI_SwitchGameModeToMissionariesAndCannibals();
  }

  public void SetMainMenuUIState(bool visible)
  {
    mainMenuPanel.SetActive(visible);
    gameplayHudPanel.SetActive(!visible);
  }

  public void UI_PlayGame()
  {
    GameManager.Instance.StateManager.SetState(nameof(IdleState));
  }

  public void UI_RestartGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
