using System.Collections.Generic;

namespace dev.vivekraman.RiverCrossing.API.Response
{
[System.Serializable]
public class JHSolveResponse
{
  public Dictionary<string, JHStageRaw> output;
  public Dictionary<string, JHStage> parsedOutput;
  public int number_of_states = -1;
}
}
