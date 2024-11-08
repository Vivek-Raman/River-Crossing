using dev.vivekraman.RiverCrossing.Core.Enums;
using UnityEngine;

namespace dev.vivekraman.RiverCrossing.Core
{
public class Character : MonoBehaviour
{
  public string Name => characterClass + ' ' + name;
  public RiverBankSide Side { get; set; } = RiverBankSide.Left;
  public CharacterClass TheCharacterClass => characterClass;

  [SerializeField] private CharacterClass characterClass = CharacterClass.Null;
  [SerializeField] private int qualifier = 0;

  private void OnMouseDown()
  {
    GameManager.Instance.TheBoat.TryToggleBoard(this);
  }
}
}
