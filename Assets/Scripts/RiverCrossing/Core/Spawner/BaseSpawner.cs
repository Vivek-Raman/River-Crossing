using dev.vivekraman.RiverCrossing.Core.Enums;
using UnityEngine;
using UnityEngine.Assertions;

namespace dev.vivekraman.RiverCrossing.Core.Spawner
{
public class BaseSpawner : MonoBehaviour
{
  [SerializeField] private CharacterClassPrefabDirectory directory;

  protected void Awake()
  {
    Assert.IsNotNull(directory);
    directory.Init();
  }

  protected void FlushAllCharacters()
  {
    GameManager gameManager = GameManager.Instance;
    foreach (GameObject character in GameObject.FindGameObjectsWithTag("Characters"))
    {
      Character characterComponent = character.GetComponent<Character>();
      gameManager.GetRiverBank(characterComponent.Side).RemoveCharacterFromAnchor(characterComponent);
      GameObject.Destroy(character);
    }
  }

  protected Character SpawnCharacterOnRiverBank(
    GameManager gameManager, CharacterClass characterClass, int qualifier, RiverBankSide side, int index)
  {
    GameObject prefab = directory.GetPrefabForCharacterClassAndQualifier(characterClass, qualifier);
    Character character = GameObject.Instantiate(prefab).GetComponent<Character>();
    character.name += index.ToString();
    Transform anchor = gameManager.GetRiverBank(side).AssignAnchorToCharacter(character);
    character.transform.position = anchor.position;
    return character;
  }

}
}
