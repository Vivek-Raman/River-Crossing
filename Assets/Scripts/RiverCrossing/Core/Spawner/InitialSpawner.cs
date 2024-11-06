using dev.vivekraman.RiverCrossing.Core.Enums;
using NUnit.Framework;
using UnityEngine;

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

  private void Start()
  {
    switch (GameManager.Instance.TheRuleEngine.TheGameMode)
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
          Character character = SpawnCharacterOnRiverBank(gameManager, CharacterClass.Missionary, side, i);
          break;
        }
        case 'C':
        {
          Character character = SpawnCharacterOnRiverBank(gameManager, CharacterClass.Cannibal, side, i);
          break;
        }
      }
    }
  }

  private void LoadInitialStateForJealousHusbands()
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
          Character character = SpawnCharacterOnRiverBank(gameManager, CharacterClass.Missionary, side, i);
          break;
        }
        case 'C':
        {
          Character character = SpawnCharacterOnRiverBank(gameManager, CharacterClass.Cannibal, side, i);
          break;
        }
      }
    }
  }

  private void FlushAllCharacters()
  {
    foreach (GameObject character in GameObject.FindGameObjectsWithTag("Characters"))
    {
      GameObject.Destroy(character);
    }
  }

  private Character SpawnCharacterOnRiverBank(
    GameManager gameManager, CharacterClass characterClass, RiverBankSide side, int index)
  {
    GameObject prefab = directory.GetPrefabForCharacterClass(characterClass);
    Character character = GameObject.Instantiate(prefab).GetComponent<Character>();
    character.name += index.ToString();
    Transform anchor = gameManager.GetRiverBank(side).AssignAnchorToCharacter(character);
    character.transform.position = anchor.position;
    return character;
  }
}
}
