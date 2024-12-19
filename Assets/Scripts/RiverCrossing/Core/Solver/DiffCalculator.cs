using System.Collections.Generic;
using dev.vivekraman.RiverCrossing.API.Response;
using UnityEngine;

namespace dev.vivekraman.RiverCrossing.Core.Solver
{
public static class DiffCalculator
{
  public static MnCStage CalculateStateDiff(MnCStage currentStage, MnCStage nextStage)
  {
    MnCStage diff = new MnCStage();
    diff.boat_position = nextStage.boat_position;
    if (diff.boat_position[0] == 'r')
    {
      diff.C_right = nextStage.C_right - currentStage.C_right;
      diff.M_right = nextStage.M_right - currentStage.M_right;
    }
    else
    {
      diff.C_left = nextStage.C_left - currentStage.C_left;
      diff.M_left = nextStage.M_left - currentStage.M_left;
    }
    Debug.Log("Current: " + currentStage.ToString() + "\nNext: " + nextStage.ToString() + "\nDiff: " + diff.ToString());
    return diff;
  }

  public static JHStage CalculateStateDiff(JHStage currentStage, JHStage nextStage)
  {
    JHStage diff = new JHStage();
    diff.boat_position = nextStage.boat_position;

    Dictionary<string, HashSet<long>> source;
    Dictionary<string, HashSet<long>> destination;
    if (diff.boat_position == "R")
    {
      source = currentStage.left_bank;
      destination = nextStage.right_bank;
    }
    else
    {
      source = currentStage.right_bank;
      destination = nextStage.left_bank;
    }

    Dictionary<string, HashSet<long>> delta = new Dictionary<string, HashSet<long>>();
    foreach ((string characterClass, HashSet<long> qualifiers) in source)
    {
      foreach (long qualifier in qualifiers)
      {
        // if character in destination, move from source to destination
        if (destination.TryGetValue(characterClass, out HashSet<long> destQualifiers))
        {
          if (destQualifiers.Contains(qualifier))
          {
            if (!delta.ContainsKey(characterClass))
            {
              delta.Add(characterClass, new HashSet<long>());
            }
            delta[characterClass].Add(qualifier);
          }
        }
      }
    }


    if (diff.boat_position == "R")
    {
      diff.right_bank = delta;
    }
    else
    {
      diff.left_bank = delta;
    }

    return diff;
  }
}
}
