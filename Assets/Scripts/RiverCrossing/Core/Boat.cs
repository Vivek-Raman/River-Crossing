using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using dev.vivekraman.RiverCrossing.Core.Enums;
using dev.vivekraman.RiverCrossing.Core.States;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;

namespace dev.vivekraman.RiverCrossing.Core
{
public class Boat : MonoBehaviour
{
  [SerializeField] private Transform leftWaypoint;
  [SerializeField] private Transform rightWaypoint;

  public RiverBankSide CurrentSide { get; private set; } = RiverBankSide.Left;
  public List<Character> CharactersOnBoard => charactersOnBoard.Values.ToList();

  private GameManager gameManager = null;
  private Dictionary<BoatSide, Transform> characterAnchors = null;
  private Dictionary<BoatSide, Character> charactersOnBoard = null;

  private void Awake()
  {
    Assert.IsNotNull(leftWaypoint);
    Assert.IsNotNull(rightWaypoint);

    this.transform.position = leftWaypoint.position;

    gameManager = GameManager.Instance;
    Assert.IsNotNull(gameManager);

    characterAnchors = new Dictionary<BoatSide, Transform>();
    Transform temp = this.transform.GetChild(0)?.GetChild(0);
    Assert.IsNotNull(temp);
    characterAnchors[BoatSide.Left1] = temp;
    temp = this.transform.GetChild(0)?.GetChild(1);
    Assert.IsNotNull(temp);
    characterAnchors[BoatSide.Left2] = temp;
    temp = this.transform.GetChild(0)?.GetChild(2);
    Assert.IsNotNull(temp);
    characterAnchors[BoatSide.Right1] = temp;
    temp = this.transform.GetChild(0)?.GetChild(3);
    Assert.IsNotNull(temp);
    characterAnchors[BoatSide.Right2] = temp;

    charactersOnBoard = new Dictionary<BoatSide, Character>();
  }

  private void OnMouseDown()
  {
    TryTraverseBoat();
  }

  public void TryToggleBoard(Character character)
  {
    if (character.Side != this.CurrentSide) return;

    if (!gameManager.CanBoardBoat && gameManager.StateManager.CurrentState.Name != nameof(SolveState)) return;

    BoatSide boatSide = charactersOnBoard.Where(entry => entry.Value.Name == character.Name)
      .Select(entry => entry.Key)
      .DefaultIfEmpty(BoatSide.Null).First();

    if (boatSide != BoatSide.Null && charactersOnBoard.Remove(boatSide))
    {
      character.Side = CurrentSide;
      character.transform.SetParent(null);
      gameManager.StateManager.SetState(nameof(CharacterBoardingState));
      character.transform.DOLocalJump(
        gameManager.GetRiverBank(CurrentSide).AssignAnchorToCharacter(character).position,
        1f, 1, 1f);
    }
    else
    {
      if (charactersOnBoard.Count >= Mathf.Min(gameManager.BoatCapacity, 4)) return;

      // board the boat
      gameManager.StateManager.SetState(nameof(CharacterBoardingState));
      gameManager.GetRiverBank(CurrentSide).RemoveCharacterFromAnchor(character);

      BoatSide side = Enum.GetValues(typeof(BoatSide)).Cast<BoatSide>()
        .FirstOrDefault(it => it != BoatSide.Null &&
                              (!charactersOnBoard.ContainsKey(it) ||
                               charactersOnBoard[it] == null));
      if (side == BoatSide.Null) return;
      charactersOnBoard.Add(side, character);
      character.transform.SetParent(characterAnchors[side]);
      character.transform.DOLocalJump(Vector3.zero, 1f, 1, 1f);
    }
  }

  private void TryTraverseBoat()
  {
    if (!gameManager.CanBoatMove) return;
    if (charactersOnBoard.Count <= 0) return;

    gameManager.StateManager.SetState(nameof(BoatTraversingState));

    CurrentSide = CurrentSide == RiverBankSide.Left ? RiverBankSide.Right : RiverBankSide.Left;
    foreach (Character character in charactersOnBoard.Values)
    {
      character.Side = CurrentSide;
    }

    switch (CurrentSide)
    {
      case RiverBankSide.Left:
        this.transform.DOLocalMove(leftWaypoint.position, 1f);
        break;
      case RiverBankSide.Right:
        this.transform.DOLocalMove(rightWaypoint.position, 1f);
        break;
    }

    bool valid = gameManager.TheRuleEngine.TryValidateRules(this);
    if (valid)
    {
      gameManager.TheScoreController.AddScore();
    }
    else
    {
      gameManager.StateManager.SetState(nameof(GameOverState));
    }
  }

  public void ForceBoatToBank(RiverBankSide side)
  {
    switch (side)
    {
      case RiverBankSide.Left:
        this.transform.DOMove(leftWaypoint.position, 1f);
        break;
      case RiverBankSide.Right:
        this.transform.DOMove(rightWaypoint.position, 1f);
        break;
    }
    CurrentSide = CurrentSide == RiverBankSide.Left ? RiverBankSide.Right : RiverBankSide.Left;
  }

  public IEnumerator ForceAlightAll()
  {
    if (charactersOnBoard.TryGetValue(BoatSide.Left1, out Character left1))
    {
      left1.transform.DOJump(gameManager.GetRiverBank(CurrentSide).AssignAnchorToCharacter(left1).position, 1f, 1, 1f);
      left1.Side = CurrentSide;
      left1.transform.SetParent(null);
      charactersOnBoard.Remove(BoatSide.Left1);
      yield return new WaitForSeconds(1f);
    }
    if (charactersOnBoard.TryGetValue(BoatSide.Right1, out Character right1))
    {
      right1.transform.DOJump(gameManager.GetRiverBank(CurrentSide).AssignAnchorToCharacter(right1).position, 1f, 1, 1f);
      right1.Side = CurrentSide;
      right1.transform.SetParent(null);
      charactersOnBoard.Remove(BoatSide.Right1);
      yield return new WaitForSeconds(1f);
    }
    if (charactersOnBoard.TryGetValue(BoatSide.Left2, out Character left2))
    {
      left2.transform.DOJump(gameManager.GetRiverBank(CurrentSide).AssignAnchorToCharacter(left2).position, 1f, 1, 1f);
      left2.Side = CurrentSide;
      left2.transform.SetParent(null);
      charactersOnBoard.Remove(BoatSide.Left2);
      yield return new WaitForSeconds(1f);
    }
    if (charactersOnBoard.TryGetValue(BoatSide.Right2, out Character right2))
    {
      right2.transform.DOJump(gameManager.GetRiverBank(CurrentSide).AssignAnchorToCharacter(right2).position, 1f, 1, 1f);
      right2.Side = CurrentSide;
      right2.transform.SetParent(null);
      charactersOnBoard.Remove(BoatSide.Right2);
      yield return new WaitForSeconds(1f);
    }
  }
}
}
