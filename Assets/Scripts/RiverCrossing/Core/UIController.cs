using dev.vivekraman.RiverCrossing.Core.Enums;
using dev.vivekraman.RiverCrossing.Core.States;
using dev.vivekraman.RiverCrossing.Utils.StateManagement;
using TMPro;
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
  [SerializeField] private GameObject solutionViewerPanel = null;
  [SerializeField] private GameObject solveButton = null;

  private void Awake()
  {
    Assert.IsNotNull(mainMenuPanel);
    Assert.IsNotNull(gameplayHudPanel);
    Assert.IsNotNull(gameOverPanel);
    Assert.IsNotNull(solveButton);
    Assert.IsNotNull(solutionViewerPanel);
  }

  private void Start()
  {
    UI_SwitchGameModeToMissionariesAndCannibals();
    gameOverPanel.SetActive(false);
    solutionViewerPanel.SetActive(false);
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

  public void SetLoaderUIState(bool active)
  {
    // TODO: loading panel
    // loaderPanel.SetActive(active);
  }

  private void SwitchGameMode(GameMode gameMode)
  {
    GameManager.Instance.TheRuleEngine.TheGameMode = gameMode;
    GameManager.Instance.Spawner.LoadInitialStateForGameMode(gameMode);
  }

  public void UI_PlayGame()
  {
    GameManager.Instance.StateManager.SetState(nameof(IdleState));
  }

  public void UI_RestartGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public void UI_MoveToSolveMode()
  {
    GameManager.Instance.StateManager.SetState(nameof(SolveState));
    solveButton.SetActive(false);
    solutionViewerPanel.SetActive(true);
  }

  public void UI_SwitchGameModeToMissionariesAndCannibals()
  {
    SwitchGameMode(GameMode.MissionariesAndCannibals);
  }

  public void UI_SwitchGameModeToJealousHusbands()
  {
    SwitchGameMode(GameMode.JealousHusbands);
  }

  public void UI_SolverStepForward()
  {
    GameManager.Instance.Solver.StepThrough(1);
  }

  public void UI_SolverStepBackward()
  {
    GameManager.Instance.Solver.StepThrough(-1);
  }
}
}
