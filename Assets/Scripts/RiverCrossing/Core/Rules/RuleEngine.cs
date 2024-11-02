using System;
using System.Collections.Generic;
using System.Linq;
using dev.vivekraman.RiverCrossing.Core.Enums;
using UnityEngine;

namespace dev.vivekraman.RiverCrossing.Core.Rules
{
public class RuleEngine : MonoBehaviour
{
  [SerializeField] private List<Rule> rules;

  public bool ShouldEvaluateRules { get; set; } = true;

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
    foreach (Rule rule in rules)
    {
      List<Character> charactersOnBank = riverBank.FetchBankedCharacters();

      int numberOfCharactersOfClass1 = 0;
      int numberOfCharactersOfClass2 = 0;
      foreach (Character character in charactersOnBank)
      {
        if (character.TheCharacterClass == rule.Class1) ++numberOfCharactersOfClass1;
        else if (character.TheCharacterClass == rule.Class2) ++numberOfCharactersOfClass2;
      }

      switch (rule.Comparator)
      {
        case Comparator.GreaterThan:
        {
          if (numberOfCharactersOfClass1 < numberOfCharactersOfClass2) return false;
          break;
        }
        case Comparator.LessThan:
        {
          if (numberOfCharactersOfClass1 > numberOfCharactersOfClass2) return false;
          break;
        }
        case Comparator.EqualTo:
        {
          if (numberOfCharactersOfClass1 != numberOfCharactersOfClass2) return false;
          break;
        }
      }
    }

    return true;
  }
}
}
