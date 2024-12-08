using System;
using System.Collections.Generic;
using dev.vivekraman.RiverCrossing.API;
using dev.vivekraman.RiverCrossing.API.Request;
using dev.vivekraman.RiverCrossing.API.Response;
using dev.vivekraman.RiverCrossing.Core.Enums;
using dev.vivekraman.RiverCrossing.Core.Spawner;
using UnityEngine;

namespace dev.vivekraman.RiverCrossing.Core.Solver
{
public class GameSolver : BaseSpawner
{
  private Dictionary<string, MnCStage> solutionMnC = null;
  private Dictionary<string, JHStage> solutionJH = null;
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
    MnCSolveRequest request = new();
    StartCoroutine(APIClient.FetchMnCSolution(request, OnMnCSolutionReceived));
  }

  private void OnMnCSolutionReceived(Dictionary<string, MnCStage> response)
  {
    loading = false;
    GameManager.Instance.TheUIController.SetLoaderUIState(loading);
    if (response == null)
    {
      // TODO: handle error
      Debug.LogError("Response is null");
      return;
    }
    solutionMnC = response;
  }

  private void SolveJealousHusbands()
  {
    JHSolveRequest request = new();
    StartCoroutine(APIClient.FetchJHSolution(request, OnJHSolutionReceived));
  }

  private void OnJHSolutionReceived(Dictionary<string, JHStage> response)
  {
    loading = false;
    GameManager.Instance.TheUIController.SetLoaderUIState(loading);
    if (response == null)
    {
      // TODO: handle error
      Debug.LogError("Response is null");
      return;
    }
    solutionJH = response;
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

    if (!solutionMnC.TryGetValue((index + increment).ToString(), out MnCStage nextStage))
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

    gameManager.TheBoat.ForceBoatToBank(ParseRiverBankSide(nextStage.boat_position));
    index += increment;
  }

  private void StepThroughJealousHusbands(int increment)
  {
    CharacterClass ParseJH(string c)
    {
      switch (c)
      {
        case "H":
          return CharacterClass.Husband;
        case "W":
          return CharacterClass.Wife;
      }

      return CharacterClass.Null;
    }

    GameManager gameManager = GameManager.Instance;

    if (!solutionJH.TryGetValue((index + increment).ToString(), out JHStage nextStage))
    {
      return;
    }

    FlushAllCharacters();
    int counter = 0;
    foreach ((string charClass, HashSet<long> qualifiers) in nextStage.left_bank)
    {
      foreach (long qualifier in qualifiers)
      {
        SpawnCharacterOnRiverBank(gameManager, ParseJH(charClass), (int) qualifier, RiverBankSide.Left, counter++);
      }
    }
    foreach ((string charClass, HashSet<long> qualifiers) in nextStage.right_bank)
    {
      foreach (long qualifier in qualifiers)
      {
        SpawnCharacterOnRiverBank(gameManager, ParseJH(charClass), (int)qualifier, RiverBankSide.Right, counter++);
      }
    }

    gameManager.TheBoat.ForceBoatToBank(ParseRiverBankSide(nextStage.boat_position));
    index += increment;
  }

  private static RiverBankSide ParseRiverBankSide(string boatPos)
  {
    foreach (RiverBankSide side in Enum.GetValues(typeof(RiverBankSide)))
    {
      if (side.ToString().ToLower() == boatPos.ToLower() ||
          side.ToString().ToLower()[0] == boatPos.ToLower()[0])
      {
        return side;
      }
    }

    return RiverBankSide.Null;
  }
}
}
