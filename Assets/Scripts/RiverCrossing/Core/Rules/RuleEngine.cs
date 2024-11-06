using dev.vivekraman.RiverCrossing.Core.Enums;
using UnityEngine;

namespace dev.vivekraman.RiverCrossing.Core.Rules
{
public class RuleEngine : MonoBehaviour
{
  public bool ShouldEvaluateRules { get; set; } = true;
  public GameMode TheGameMode => gameMode;

  [SerializeField] private GameMode gameMode = GameMode.MissionariesAndCannibals;

  public bool TryValidateRules()
  {
    if (!ShouldEvaluateRules) return true;
    bool valid = true;

    GameManager gameManager = GameManager.Instance;
    RiverBank leftBank = gameManager.GetRiverBank(RiverBankSide.Left);
    valid = RunRulesOnRiverBank(leftBank);
    if (!valid) return false;
    RiverBank rightBank = gameManager.GetRiverBank(RiverBankSide.Right);
    valid = RunRulesOnRiverBank(rightBank);
    return valid;
  }

  private bool RunRulesOnRiverBank(RiverBank riverBank)
  {
    switch (gameMode)
    {
      case GameMode.MissionariesAndCannibals:
      {
        int missionaryCount = 0;
        int cannibalCount = 0;
        foreach (Character character in riverBank.FetchBankedCharacters())
        {
          switch (character.TheCharacterClass)
          {
            case CharacterClass.Cannibal:
              ++cannibalCount;
              break;
            case CharacterClass.Missionary:
              ++missionaryCount;
              break;
          }
        }

        return missionaryCount <= cannibalCount;
      }
      case GameMode.JealousHusbands:
        // TODO: implement
        return true;
    }
    return true;
  }
}
}
