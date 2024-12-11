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
    for (int x = 0; x < gameManager.CharacterCount; ++x)
    {
      SpawnCharacterOnRiverBank(gameManager, CharacterClass.Missionary, 0, RiverBankSide.Left, index++);
      SpawnCharacterOnRiverBank(gameManager, CharacterClass.Cannibal, 0, RiverBankSide.Left, index++);
    }
  }

  private void LoadInitialStateForJealousHusbands()
  {
    FlushAllCharacters();

    GameManager gameManager = GameManager.Instance;
    int index = 0;
    for (int q = 0; q < gameManager.CharacterCount; ++q)
    {
      SpawnCharacterOnRiverBank(gameManager, CharacterClass.Wife, q + 1, RiverBankSide.Left, index++);
      SpawnCharacterOnRiverBank(gameManager, CharacterClass.Husband, q + 1, RiverBankSide.Left, index++);
    }
  }
}
}
