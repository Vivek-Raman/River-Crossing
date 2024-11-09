using dev.vivekraman.RiverCrossing.Core.Enums;
using UnityEngine;
using UnityEngine.Assertions;

namespace dev.vivekraman.RiverCrossing.Core.Spawner
{
public class InitialSpawner : MonoBehaviour
{
  [SerializeField] private CharacterClassPrefabDirectory directory;

  private void Awake()
  {
    Assert.IsNotNull(directory);
    directory.Init();
  }

  public void LoadInitialStateForGameMode(GameMode gameMode)
  {
    switch (gameMode)
    {
      case GameMode.MissionariesAndCannibals:
        LoadInitialStateForMissionariesAndCannibals();
        break;
      case GameMode.JealousHusbands:
        LoadInitialStateForJealousHusbands();
        break;
    }
  }

  private void LoadInitialStateForMissionariesAndCannibals()
  {
    FlushAllCharacters();

    string initialState = "MMMCCC_";

    GameManager gameManager = GameManager.Instance;
    RiverBankSide side = RiverBankSide.Left;
    for (int i = 0; i < initialState.Length; ++i)
    {
      switch (initialState[i])
      {
        case '_':
        {
          side = RiverBankSide.Right;
          break;
        }
        case 'M':
        {
          Character character = SpawnCharacterOnRiverBank(gameManager, CharacterClass.Missionary, 0, side, i);
          break;
        }
        case 'C':
        {
          Character character = SpawnCharacterOnRiverBank(gameManager, CharacterClass.Cannibal, 0, side, i);
          break;
        }
      }
    }
  }

  private void LoadInitialStateForJealousHusbands()
  {
    FlushAllCharacters();

    // TODO: string initialState = "";
    GameManager gameManager = GameManager.Instance;
    int index = 0;
    SpawnCharacterOnRiverBank(gameManager, CharacterClass.Wife, 1, RiverBankSide.Left, index++);
    SpawnCharacterOnRiverBank(gameManager, CharacterClass.Husband, 1, RiverBankSide.Left, index++);
    SpawnCharacterOnRiverBank(gameManager, CharacterClass.Wife, 2, RiverBankSide.Left, index++);
    SpawnCharacterOnRiverBank(gameManager, CharacterClass.Husband, 2, RiverBankSide.Left, index++);
    SpawnCharacterOnRiverBank(gameManager, CharacterClass.Wife, 3, RiverBankSide.Left, index++);
    SpawnCharacterOnRiverBank(gameManager, CharacterClass.Husband, 3, RiverBankSide.Left, index++);

  }

  private void FlushAllCharacters()
  {
    GameManager gameManager = GameManager.Instance;
    foreach (GameObject character in GameObject.FindGameObjectsWithTag("Characters"))
    {
      Character characterComponent = character.GetComponent<Character>();
      gameManager.GetRiverBank(characterComponent.Side).RemoveCharacterFromAnchor(characterComponent);
      GameObject.Destroy(character);
    }
  }

  private Character SpawnCharacterOnRiverBank(
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
