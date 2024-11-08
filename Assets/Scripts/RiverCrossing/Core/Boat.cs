using System.Collections.Generic;
using System.Linq;
using dev.vivekraman.RiverCrossing.Core.Enums;
using dev.vivekraman.RiverCrossing.Core.States;
using DG.Tweening;
using TMPro;
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
      character.transform.DOLocalMove(
        gameManager.GetRiverBank(CurrentSide).AssignAnchorToCharacter(character).position, 1f);
    }
    else if (charactersOnBoard.TryGetValue(BoatSide.Right, out Character rightCharacter) &&
             rightCharacter.Name == character.Name)
    {
      // alight the boat on the right bank
      charactersOnBoard.Remove(BoatSide.Right);
      character.Side = CurrentSide;
      character.transform.SetParent(null);
      gameManager.StateManager.SetState(nameof(CharacterBoardingState));
      character.transform.DOLocalMove(
        gameManager.GetRiverBank(CurrentSide).AssignAnchorToCharacter(character).position, 1f);
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
      character.transform.DOLocalMove(Vector3.zero, 1f);
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
    // TODO: remove debug UI
    GameObject.Find("Debug Text").GetComponent<TMP_Text>().text = "Valid: " + valid.ToString();
  }
}
}
