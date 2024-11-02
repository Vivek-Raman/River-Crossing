using System;
using System.Collections.Generic;
using dev.vivekraman.RiverCrossing.Core.Enums;
using UnityEngine;

namespace dev.vivekraman.RiverCrossing.Core.Spawner
{
[CreateAssetMenu]
public class CharacterClassPrefabDirectory : ScriptableObject
{
  [SerializeField] private List<CharacterClassPrefab> directory;

  private Dictionary<CharacterClass, GameObject> map = null;

  public void Init()
  {
    map = new Dictionary<CharacterClass, GameObject>();
    foreach (CharacterClassPrefab entry in directory)
    {
      map.Add(entry.CharacterClass, entry.Prefab);
    }
  }

  public GameObject GetPrefabForCharacterClass(CharacterClass characterClass)
  {
    if (!map.TryGetValue(characterClass, out GameObject value))
    {
      throw new KeyNotFoundException(characterClass.ToString());
    }

    return value;
  }
}
}
