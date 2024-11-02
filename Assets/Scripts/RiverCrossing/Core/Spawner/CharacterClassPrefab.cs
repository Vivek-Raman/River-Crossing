using System;
using dev.vivekraman.RiverCrossing.Core.Enums;
using UnityEngine;

namespace dev.vivekraman.RiverCrossing.Core.Spawner
{
[Serializable]
public class CharacterClassPrefab
{
  public CharacterClass CharacterClass => characterClass;
  public GameObject Prefab => prefab;

  [SerializeField] private CharacterClass characterClass;
  [SerializeField] private GameObject prefab;
}
}
