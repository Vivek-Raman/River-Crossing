using System;
using dev.vivekraman.RiverCrossing.Core.Enums;
using UnityEngine;

namespace dev.vivekraman.RiverCrossing.Core.Spawner
{
[Serializable]
public class CharacterClassPrefab
{
  public CharacterClass CharacterClass => characterClass;
  public int Qualifier => qualifier;
  public GameObject Prefab => prefab;

  [SerializeField] private CharacterClass characterClass;
  [SerializeField] private int qualifier;
  [SerializeField] private GameObject prefab;
}
}
