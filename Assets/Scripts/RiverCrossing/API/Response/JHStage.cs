using System.Collections.Generic;

namespace dev.vivekraman.RiverCrossing.API.Response
{
[System.Serializable]
public class JHStage
{
  public Dictionary<string, HashSet<long>> left_bank = new ();
  public Dictionary<string, HashSet<long>> right_bank = new ();
  public string boat_position = "";

  public static JHStage FromRaw(JHStageRaw raw)
  {
    JHStage stage = new ();
    foreach (object[] character in raw.left_bank)
    {
      if (character[0] == null || character[1] is not long) continue;

      if (!stage.left_bank.ContainsKey((character[0] as string)!))
      {
        stage.left_bank.Add((character[0] as string)!, new HashSet<long>());
      }
      stage.left_bank[(character[0] as string)!].Add((long)character[1]);
    }
    foreach (object[] character in raw.right_bank)
    {
      if (character[0] == null || character[1] is not long) continue;

      if (!stage.right_bank.ContainsKey((character[0] as string)!))
      {
        stage.right_bank.Add((character[0] as string)!, new HashSet<long>());
      }
      stage.right_bank[(character[0] as string)!].Add((long)character[1]);
    }
    stage.boat_position = raw.boat_position;

    return stage;
  }
}
}
