using System.Collections.Generic;
using dev.vivekraman.RiverCrossing.Core.Enums;
using UnityEngine;

namespace dev.vivekraman.RiverCrossing.Core.Spawner
{
[CreateAssetMenu]
public class CharacterClassPrefabDirectory : ScriptableObject
{
  [SerializeField] private List<CharacterClassPrefab> directory;

  private Dictionary<string, GameObject> map = null;

  public void Init()
  {
    map = new Dictionary<string, GameObject>();
    foreach (CharacterClassPrefab entry in directory)
    {
      string key = entry.CharacterClass.ToString() + entry.Qualifier.ToString();
      map.Add(key, entry.Prefab);
    }
  }

  public GameObject GetPrefabForCharacterClassAndQualifier(CharacterClass characterClass, int qualifier)
  {
    // if (!initialized) Init();
    string key = characterClass.ToString() + qualifier.ToString();
    if (!map.TryGetValue(key, out GameObject value))
    {
      throw new KeyNotFoundException(key);
    }

    return value;
  }
}
}
