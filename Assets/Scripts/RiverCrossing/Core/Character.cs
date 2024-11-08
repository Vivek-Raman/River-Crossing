using System;
using dev.vivekraman.RiverCrossing.Core.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace dev.vivekraman.RiverCrossing.Core
{
public class Character : MonoBehaviour
{
  public string Name => characterClass + ' ' + name;
  public RiverBankSide Side { get; set; } = RiverBankSide.Left;
  public CharacterClass TheCharacterClass => characterClass;

  [SerializeField] private CharacterClass characterClass = CharacterClass.Null;
  [SerializeField] private int qualifier = 0;

  private void Start()
  {
    if (characterClass == CharacterClass.Cannibal) return;
    Animator animator = this.GetComponentInChildren<Animator>();
    animator.SetFloat("AnimOffset", Random.Range(-1f, 1f));
  }

  private void OnMouseDown()
  {
    GameManager.Instance.TheBoat.TryToggleBoard(this);
  }
}
}
