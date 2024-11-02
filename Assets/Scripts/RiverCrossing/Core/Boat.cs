using System;
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
  private Transform leftAnchor = null;
  private Transform rightAnchor = null;
  private Dictionary<BoatSide, Character> charactersOnBoard = null;

  private void Awake()
  {
    Assert.IsNotNull(leftWaypoint);
    Assert.IsNotNull(rightWaypoint);

    gameManager = GameManager.Instance;
    Assert.IsNotNull(gameManager);

    rightAnchor = this.transform.GetChild(0)?.GetChild(0);
    Assert.IsNotNull(rightAnchor);
    leftAnchor = this.transform.GetChild(0)?.GetChild(1);
    Assert.IsNotNull(leftAnchor);

    charactersOnBoard = new();
  }

  private void OnMouseDown()
  {
    TryTraverseBoat();
  }

  public void TryToggleBoard(Character character)
  {
    if (character.Side != this.CurrentSide) return;

    if (!gameManager.CanBoardBoat) return;

    if (charactersOnBoard.TryGetValue(BoatSide.Left, out Character leftCharacter) &&
        leftCharacter.Name == character.Name)
    {
      // alight the boat on the left side
      charactersOnBoard.Remove(BoatSide.Left);
      character.transform.SetParent(null);
      gameManager.StateManager.SetState(nameof(CharacterBoardingState));
      // TODO: tween / animate
      character.transform.position = gameManager.GetRiverBank(CurrentSide).GetCharacterAnchor().position;
    }
    else if (charactersOnBoard.TryGetValue(BoatSide.Right, out Character rightCharacter) &&
             rightCharacter.Name == character.Name)
    {
      // alight the boat on the right side
      charactersOnBoard.Remove(BoatSide.Right);
      character.Side = CurrentSide;
      character.transform.SetParent(null);
      gameManager.StateManager.SetState(nameof(CharacterBoardingState));
      // TODO: tween / animate
      character.transform.position = gameManager.GetRiverBank(CurrentSide).GetCharacterAnchor().position;
    }
    else
    {
      if (charactersOnBoard.Count >= 2) return;

      // board the boat
      gameManager.StateManager.SetState(nameof(CharacterBoardingState));
      BoatSide side = charactersOnBoard.ContainsKey(BoatSide.Left) ? BoatSide.Right : BoatSide.Left;
      charactersOnBoard.Add(side, character);
      Transform anchor = side == BoatSide.Left ? leftAnchor : rightAnchor;
      character.transform.SetParent(anchor);
      // TODO: tween / animate
      character.transform.SetLocalPositionAndRotation(Vector3.zero, character.transform.rotation);
    }
    // TODO:
    // TODO:
  }

  public void TryTraverseBoat()
  {
    if (!gameManager.CanBoatMove)
    {
      return;
    }

    // TODO: update state to traversing
    gameManager.StateManager.SetState(nameof(BoatTraversingState));

    CurrentSide = CurrentSide == RiverBankSide.Left ? RiverBankSide.Right : RiverBankSide.Left;
    foreach (Character character in charactersOnBoard.Values)
    {
      character.Side = CurrentSide;
    }

    // TODO: tween between leftWaypoint and rightWaypoint
    switch (CurrentSide)
    {
      case RiverBankSide.Left:
        this.transform.position = leftWaypoint.position;
        break;
      case RiverBankSide.Right:
        this.transform.position = rightWaypoint.position;
        break;
    }
  }
}
}
