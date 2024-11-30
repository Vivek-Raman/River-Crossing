using System.Collections.Generic;
using dev.vivekraman.RiverCrossing.API;
using dev.vivekraman.RiverCrossing.API.Response;
using dev.vivekraman.RiverCrossing.Core.Enums;
using dev.vivekraman.RiverCrossing.Core.Spawner;
using UnityEngine;

namespace dev.vivekraman.RiverCrossing.Core.Solver
{
public class GameSolver : BaseSpawner
{
  private Dictionary<string, MnCStage> solution = null;
  private int index = 0;
  private bool loading = false;

  public void Solve()
  {
    index = 0;
    switch (GameManager.Instance.TheRuleEngine.TheGameMode)
    {
      case GameMode.MissionariesAndCannibals:
        SolveMissionariesAndCannibals();
        break;
      case GameMode.JealousHusbands:
        SolveJealousHusbands();
        break;
    }
  }

  private void SolveMissionariesAndCannibals()
  {
    GameManager gameManager = GameManager.Instance;

    MnCStage currentStage = new MnCStage();
    currentStage.boat_position = gameManager.TheBoat.CurrentSide.ToString().ToLower();

    foreach (Character character in gameManager.GetRiverBank(RiverBankSide.Left).FetchBankedCharacters())
    {
      switch (character.TheCharacterClass)
      {
        case CharacterClass.Cannibal:
          currentStage.C_left++;
          break;
        case CharacterClass.Missionary:
          currentStage.M_left++;
          break;
      }
    }

    foreach (Character character in gameManager.GetRiverBank(RiverBankSide.Right).FetchBankedCharacters())
    {
      switch (character.TheCharacterClass)
      {
        case CharacterClass.Cannibal:
          currentStage.C_right++;
          break;
        case CharacterClass.Missionary:
          currentStage.M_right++;
          break;
      }
    }

    loading = true;
    gameManager.TheUIController.SetLoaderUIState(loading);
    StartCoroutine(APIClient.FetchMnCSolution(currentStage, OnMnCSolutionReceived));
  }

  private void OnMnCSolutionReceived(MnCSolutionResponse response)
  {
    loading = false;
    GameManager.Instance.TheUIController.SetLoaderUIState(loading);
    if (response == null)
    {
      // TODO: handle error
      Debug.LogError("Response is null");
      return;
    }
    solution = response.output;
  }

  public void SolveJealousHusbands()
  {
    throw new System.NotImplementedException();
  }

  /// <summary>
  /// Moves all game objects to the state defined by <c>index + increment</c>
  /// </summary>
  /// <param name="increment">Use only -1 or 1, to step backwards or forwards, respectively.</param>
  public void StepThrough(int increment)
  {
    if (loading) return;
    GameManager gameManager = GameManager.Instance;
    switch (gameManager.TheRuleEngine.TheGameMode)
    {
      case GameMode.MissionariesAndCannibals:
        StepThroughMissionariesAndCannibals(increment);
        break;
      case GameMode.JealousHusbands:
        StepThroughJealousHusbands(increment);
        break;
    }
  }

  private void StepThroughMissionariesAndCannibals(int increment)
  {
    GameManager gameManager = GameManager.Instance;

    if (!solution.TryGetValue((index + increment).ToString(), out MnCStage nextStage))
    {
      return;
    }

    FlushAllCharacters();
    int counter = 0;
    for (int i = 0; i < nextStage.M_left; ++i)
    {
      SpawnCharacterOnRiverBank(gameManager, CharacterClass.Missionary, 0, RiverBankSide.Left, counter++);
    }
    for (int i = 0; i < nextStage.C_left; ++i)
    {
      SpawnCharacterOnRiverBank(gameManager, CharacterClass.Cannibal, 0, RiverBankSide.Left, counter++);
    }
    for (int i = 0; i < nextStage.M_right; ++i)
    {
      SpawnCharacterOnRiverBank(gameManager, CharacterClass.Missionary, 0, RiverBankSide.Right, counter++);
    }
    for (int i = 0; i < nextStage.C_right; ++i)
    {
      SpawnCharacterOnRiverBank(gameManager, CharacterClass.Cannibal, 0, RiverBankSide.Right, counter++);
    }

    gameManager.TheBoat.ForceBoatToBank(nextStage.ParseRiverBankSide());
    index += increment;
  }

  private void StepThroughJealousHusbands(int increment)
  {
    throw new System.NotImplementedException();
  }
}
}
