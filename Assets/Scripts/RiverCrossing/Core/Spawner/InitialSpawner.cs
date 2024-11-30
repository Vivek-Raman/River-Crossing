using System;
using dev.vivekraman.RiverCrossing.Core.Enums;

namespace dev.vivekraman.RiverCrossing.Core.Spawner
{
public class InitialSpawner : BaseSpawner
{
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

    GameManager gameManager = GameManager.Instance;
    int index = 0;
    SpawnCharacterOnRiverBank(gameManager, CharacterClass.Missionary, 0, RiverBankSide.Left, index++);
    SpawnCharacterOnRiverBank(gameManager, CharacterClass.Missionary, 0, RiverBankSide.Left, index++);
    SpawnCharacterOnRiverBank(gameManager, CharacterClass.Missionary, 0, RiverBankSide.Left, index++);
    SpawnCharacterOnRiverBank(gameManager, CharacterClass.Cannibal, 0, RiverBankSide.Left, index++);
    SpawnCharacterOnRiverBank(gameManager, CharacterClass.Cannibal, 0, RiverBankSide.Left, index++);
    SpawnCharacterOnRiverBank(gameManager, CharacterClass.Cannibal, 0, RiverBankSide.Left, index++);
  }

  private void LoadInitialStateForJealousHusbands()
  {
    FlushAllCharacters();

    GameManager gameManager = GameManager.Instance;
    int index = 0;
    SpawnCharacterOnRiverBank(gameManager, CharacterClass.Wife, 1, RiverBankSide.Left, index++);
    SpawnCharacterOnRiverBank(gameManager, CharacterClass.Husband, 1, RiverBankSide.Left, index++);
    SpawnCharacterOnRiverBank(gameManager, CharacterClass.Wife, 2, RiverBankSide.Left, index++);
    SpawnCharacterOnRiverBank(gameManager, CharacterClass.Husband, 2, RiverBankSide.Left, index++);
    SpawnCharacterOnRiverBank(gameManager, CharacterClass.Wife, 3, RiverBankSide.Left, index++);
    SpawnCharacterOnRiverBank(gameManager, CharacterClass.Husband, 3, RiverBankSide.Left, index++);
  }
}
}
