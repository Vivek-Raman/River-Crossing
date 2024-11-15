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
  [SerializeField] private GameObject gameOverPanel = null;

  private void Awake()
  {
    Assert.IsNotNull(mainMenuPanel);
    Assert.IsNotNull(gameplayHudPanel);
    Assert.IsNotNull(gameOverPanel);
  }

  private void Start()
  {
    UI_SwitchGameModeToMissionariesAndCannibals();
    gameOverPanel.SetActive(false);
  }

  public void SetMainMenuUIState(bool visible)
  {
    mainMenuPanel.SetActive(visible);
    gameplayHudPanel.SetActive(!visible);
  }

  public void ShowGameOverPanel()
  {
    gameOverPanel.SetActive(true);
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
