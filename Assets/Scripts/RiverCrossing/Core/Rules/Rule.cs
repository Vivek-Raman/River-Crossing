using dev.vivekraman.RiverCrossing.Core.Enums;
using UnityEngine;

namespace dev.vivekraman.RiverCrossing.Core.Rules
{
[CreateAssetMenu]
public class Rule : ScriptableObject
{
  [SerializeField] private CharacterClass class1;
  [SerializeField] private Comparator comparator;
  [SerializeField] private CharacterClass class2;

  public CharacterClass Class1 => class1;
  public Comparator Comparator => comparator;
  public CharacterClass Class2 => class2;
}
}
