using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
  public TraversalMode Traversal { get; set; } = TraversalMode.BreadthFirst;
  public int StateCount { get; private set; } = 0;

  private Dictionary<string, MnCStage> solutionMnC = null;
  private Dictionary<string, JHStage> solutionJH = null;

  private int index = 0;
  private bool loading = false;

  public IEnumerator Solve()
  {
    if (loading) yield break;
    index = 0;
    switch (GameManager.Instance.TheRuleEngine.TheGameMode)
    {
      case GameMode.MissionariesAndCannibals:
        yield return SolveMissionariesAndCannibals();
        break;
      case GameMode.JealousHusbands:
        yield return SolveJealousHusbands();
        break;
    }
  }

  private IEnumerator SolveMissionariesAndCannibals()
  {
    GameManager gameManager = GameManager.Instance;

    MnCSolveRequest request = new MnCSolveRequest
    {
      M_total = gameManager.CharacterCount,
      C_total = gameManager.CharacterCount,
      boat_capacity = gameManager.BoatCapacity,
      boat_position = gameManager.TheBoat.CurrentSide.ToString().ToLower(),
    };
    request.SetSolver(Traversal);

    foreach (Character character in gameManager.GetRiverBank(RiverBankSide.Left).FetchBankedCharacters())
    {
      switch (character.TheCharacterClass)
      {
        case CharacterClass.Cannibal:
          request.C_left++;
          break;
        case CharacterClass.Missionary:
          request.M_left++;
          break;
      }
    }

    foreach (Character character in gameManager.GetRiverBank(RiverBankSide.Right).FetchBankedCharacters())
    {
      switch (character.TheCharacterClass)
      {
        case CharacterClass.Cannibal:
          request.C_right++;
          break;
        case CharacterClass.Missionary:
          request.M_right++;
          break;
      }
    }

    loading = true;
    gameManager.TheUIController.SetLoaderUIState(loading);
    yield return APIClient.FetchMnCSolution(request, OnMnCSolutionReceived);
  }

  private void OnMnCSolutionReceived(MnCSolveResponse response)
  {
    loading = false;
    GameManager.Instance.TheUIController.SetLoaderUIState(loading);
    if (response == null)
    {
      // TODO: handle error
      Debug.LogError("Response is null");
      return;
    }

    if (response.output == null)
    {
      // no solution exists
    }

    solutionMnC = response.output;
    StateCount = response.number_of_states - 1;
  }

  private IEnumerator SolveJealousHusbands()
  {
    GameManager gameManager = GameManager.Instance;
    JHSolveRequest request = new()
    {
      num_of_couples = gameManager.CharacterCount,
      boat_capacity = gameManager.BoatCapacity,
    };
    request.SetSolver(gameManager.Solver.Traversal);

    List<Character> leftCharacters = gameManager.GetRiverBank(RiverBankSide.Left).FetchBankedCharacters();
    List<Character> rightCharacters = gameManager.GetRiverBank(RiverBankSide.Right).FetchBankedCharacters();
    request.stage = new JHStageRaw
    {
      boat_position = gameManager.TheBoat.CurrentSide.ToString().ToUpper()[..1],
      left_bank = new object[leftCharacters.Count][],
      right_bank = new object[rightCharacters.Count][],
    };

    int count = 0;
    foreach (Character character in leftCharacters)
    {
      request.stage.left_bank[count++] = new object[]
      {
        character.TheCharacterClass.ToString()[0],
        character.Qualifier,
      };
    }
    count = 0;
    foreach (Character character in rightCharacters)
    {
      request.stage.right_bank[count++] = new object[]
      {
        character.TheCharacterClass.ToString()[0],
        character.Qualifier,
      };
    }

    yield return APIClient.FetchJHSolution(request, OnJHSolutionReceived);
  }

  public int GetBestSolutionStepCount()
  {
    switch (GameManager.Instance.TheRuleEngine.TheGameMode)
    {
      case GameMode.MissionariesAndCannibals:
        return SafeGetSolutionStepCount(solutionMnC);
      case GameMode.JealousHusbands:
        return SafeGetSolutionStepCount(solutionJH);
    }

    return -1;

    int SafeGetSolutionStepCount(ICollection solution)
    {
      if (solution == null)
      {
        return -1;
      }

      return solution.Count;
    }
  }

  private void OnJHSolutionReceived(JHSolveResponse response)
  {
    loading = false;
    GameManager.Instance.TheUIController.SetLoaderUIState(loading);
    if (response == null)
    {
      // TODO: handle error
      Debug.LogError("Response is null");
      return;
    }
    solutionJH = response.parsedOutput;
    StateCount = response.number_of_states - 1;
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
    return Enum.GetValues(typeof(RiverBankSide)).Cast<RiverBankSide>()
      .FirstOrDefault(side => side.ToString().ToLower() == boatPos.ToLower() ||
                              side.ToString().ToLower()[0] == boatPos.ToLower()[0]);
  }
}
}
