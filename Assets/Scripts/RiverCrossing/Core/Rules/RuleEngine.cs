using System.Collections.Generic;
using dev.vivekraman.RiverCrossing.Core.Enums;
using UnityEngine;

namespace dev.vivekraman.RiverCrossing.Core.Rules
{
public class RuleEngine : MonoBehaviour
{
  public bool ShouldEvaluateRules { get; set; } = true;
  public GameMode TheGameMode { get; set; } = GameMode.Null;

  public bool TryValidateRules(Boat boat)
  {
    if (!ShouldEvaluateRules) return true;
    bool valid = true;

    GameManager gameManager = GameManager.Instance;
    RiverBank leftBank = gameManager.GetRiverBank(RiverBankSide.Left);
    valid = RunRulesOnRiverBank(boat, leftBank);
    if (!valid) return false;
    RiverBank rightBank = gameManager.GetRiverBank(RiverBankSide.Right);
    valid = RunRulesOnRiverBank(boat, rightBank);
    return valid;
  }

  private bool RunRulesOnRiverBank(Boat boat, RiverBank riverBank)
  {
    switch (TheGameMode)
    {
      case GameMode.MissionariesAndCannibals:
      {
        int missionaryCount = 0;
        int cannibalCount = 0;
        List<Character> characters = new List<Character>(riverBank.FetchBankedCharacters());
        if (boat.CurrentSide == riverBank.Side)
        {
          // if boat is on this side, evaluate rules such that the characters onboard are on the bank itself
          characters.AddRange(boat.CharactersOnBoard);
        }
        foreach (Character character in characters)
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

        return missionaryCount == 0 || cannibalCount == 0 || missionaryCount >= cannibalCount;
      }
      case GameMode.JealousHusbands:
      {
        List<Character> characters = new List<Character>(riverBank.FetchBankedCharacters());
        if (boat.CurrentSide == riverBank.Side)
        {
          // if boat is on this side, evaluate rules such that the characters onboard are on the bank itself
          characters.AddRange(boat.CharactersOnBoard);
        }

        foreach (Character wife in characters)
        {
          if (wife.TheCharacterClass == CharacterClass.Husband) continue;

          if (wife.TheCharacterClass == CharacterClass.Wife)
          {
            bool atRisk = false;
            foreach (Character otherHusband in characters)
            {
              // ignore all wives
              if (otherHusband.TheCharacterClass != CharacterClass.Husband) continue;

              if (otherHusband.Qualifier == wife.Qualifier)
              {
                atRisk = false;
                break;
              }
              else
              {
                atRisk = true;
              }
            }

            return !atRisk;
          }
        }

        return true;
      }
    }
    return true;
  }
}
}
