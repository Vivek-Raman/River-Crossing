using System.Collections.Generic;
using dev.vivekraman.RiverCrossing.Core.Enums;
using UnityEngine;
using UnityEngine.Assertions;

namespace dev.vivekraman.RiverCrossing.Core
{
public class RiverBank : MonoBehaviour
{
  public RiverBankSide Side => side;

  [SerializeField] private RiverBankSide side = RiverBankSide.Left;

  private Transform boatAnchor = null;
  private Dictionary<Transform, Character> bankedCharacters = null;

  private void Awake()
  {
    boatAnchor = this.transform.GetChild(0)?.transform;
    Assert.IsNotNull(boatAnchor);

    bankedCharacters = new Dictionary<Transform, Character>();
    foreach (Transform characterAnchor in this.transform.GetChild(1))
    {
      bankedCharacters.Add(characterAnchor, null);
    }
  }

  private void Start()
  {
    GameManager.Instance.RegisterRiverBank(this);
  }

  public Transform AssignAnchorToCharacter(Character character)
  {
    foreach ((Transform anchor, Character existing) in bankedCharacters)
    {
      if (existing != null) continue;

      bankedCharacters[anchor] = character;
      return anchor;
    }

    Debug.LogError("Ran out of bank anchors!");
    return this.transform;
  }

  public void RemoveCharacterFromAnchor(Character character)
  {
    Transform anchorToClear = null;
    foreach ((Transform anchor, Character existing) in bankedCharacters)
    {
      if (existing != character) continue;
      anchorToClear = anchor;
    }

    if (anchorToClear != null)
    {
      bankedCharacters[anchorToClear] = null;
    }
  }
}
}
