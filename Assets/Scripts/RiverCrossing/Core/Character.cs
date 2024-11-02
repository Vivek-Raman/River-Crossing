using dev.vivekraman.RiverCrossing.Core.Enums;
using UnityEngine;

namespace dev.vivekraman.RiverCrossing.Core
{
public class Character : MonoBehaviour
{
  public string Name => characterClass + ' ' + name;

  [SerializeField] private CharacterClass characterClass = CharacterClass.Null;

  public RiverBankSide Side { get; set; } = RiverBankSide.Left;

  private void OnMouseDown()
  {
    GameManager.Instance.TheBoat.TryToggleBoard(this);
  }
}
}
