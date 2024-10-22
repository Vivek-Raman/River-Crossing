using System.Collections.Generic;
using dev.vivekraman.RiverCrossing.Core.Enums;
using dev.vivekraman.RiverCrossing.MissionariesAndCannibals.Game.States;
using UnityEngine;
using UnityEngine.Assertions;

namespace dev.vivekraman.RiverCrossing.Core
{
public class Boat : MonoBehaviour
{
  [SerializeField] private Transform leftWaypoint;
  [SerializeField] private Transform rightWaypoint;

  public RiverBankSide CurrentSide { get; private set; } = RiverBankSide.Left;

  private GameManager gameManager = null;

  private Dictionary<string, Character> charactersOnBoard = null;
  private const int MAX_CHARACTERS_ON_BOAT = 2;

  private void Awake()
  {
    Assert.IsNotNull(leftWaypoint);
    Assert.IsNotNull(rightWaypoint);

    gameManager = GameManager.Instance;
    Assert.IsNotNull(gameManager);

    charactersOnBoard = new Dictionary<string, Character>();
  }

  public void TryToggleBoard(Character character)
  {
    if (character.Side != this.CurrentSide) return;

    if (!gameManager.CanBoardBoat) return;
    if (charactersOnBoard.ContainsKey(character.Name))
    {
      // alight the boat
      charactersOnBoard.Remove(character.Name);

      // TODO: distribute positions on river bank
      // TODO: tween / animate
      gameManager.StateManager.SetState(nameof(CharacterBoardingState));
      character.transform.position = gameManager.GetRiverBank(CurrentSide).transform.position;
    }
    else
    {
      if (charactersOnBoard.Count >= MAX_CHARACTERS_ON_BOAT) return;

      // board the boat
      gameManager.StateManager.SetState(nameof(CharacterBoardingState));
      charactersOnBoard.Add(character.Name, character);


      // TODO: fix boarded position on boat
      // TODO: tween / animate
      character.transform.position = this.transform.position + Vector3.forward;
    }
    // TODO:
    // TODO:
  }

  public void Traverse()
  {
    // TODO: update state to traversing
    // TODO: update CurrentSide
    // TODO: tween between leftWaypoint and rightWaypoint
  }
}
}
