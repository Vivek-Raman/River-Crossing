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

    charactersOnBoard = new Dictionary<BoatSide, Character>();
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
      // alight the boat on the left bank
      charactersOnBoard.Remove(BoatSide.Left);
      character.transform.SetParent(null);
      gameManager.StateManager.SetState(nameof(CharacterBoardingState));
      character.transform.DOLocalJump(
        gameManager.GetRiverBank(CurrentSide).AssignAnchorToCharacter(character).position,
        1f, 1, 1f);
    }
    else if (charactersOnBoard.TryGetValue(BoatSide.Right, out Character rightCharacter) &&
             rightCharacter.Name == character.Name)
    {
      // alight the boat on the right bank
      charactersOnBoard.Remove(BoatSide.Right);
      character.Side = CurrentSide;
      character.transform.SetParent(null);
      gameManager.StateManager.SetState(nameof(CharacterBoardingState));
      character.transform.DOLocalJump(
        gameManager.GetRiverBank(CurrentSide).AssignAnchorToCharacter(character).position,
        1f, 1, 1f);
    }
    else
    {
      if (charactersOnBoard.Count >= 2) return;

      // board the boat
      gameManager.StateManager.SetState(nameof(CharacterBoardingState));
      gameManager.GetRiverBank(CurrentSide).RemoveCharacterFromAnchor(character);
      BoatSide side = charactersOnBoard.ContainsKey(BoatSide.Left) ? BoatSide.Right : BoatSide.Left;
      charactersOnBoard.Add(side, character);
      Transform anchor = side == BoatSide.Left ? leftAnchor : rightAnchor;
      character.transform.SetParent(anchor);
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
        this.transform.position = leftWaypoint.position;
        break;
      case RiverBankSide.Right:
        this.transform.position = rightWaypoint.position;
        break;
    }
  }

  public void ForceAlightAll()
  {
    if (charactersOnBoard.TryGetValue(BoatSide.Left, out Character left))
    {
      left.transform.position = gameManager.GetRiverBank(CurrentSide).AssignAnchorToCharacter(left).position;
      left.Side = CurrentSide;
      left.transform.SetParent(null);
      charactersOnBoard.Remove(BoatSide.Left);
    }
    if (charactersOnBoard.TryGetValue(BoatSide.Right, out Character right))
    {
      right.transform.position = gameManager.GetRiverBank(CurrentSide).AssignAnchorToCharacter(right).position;
      right.Side = CurrentSide;
      right.transform.SetParent(null);
      charactersOnBoard.Remove(BoatSide.Right);
    }
  }
}
}
