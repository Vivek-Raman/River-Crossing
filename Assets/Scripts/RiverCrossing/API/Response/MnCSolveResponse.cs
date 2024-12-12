using System.Collections.Generic;

namespace dev.vivekraman.RiverCrossing.API.Response
{
[System.Serializable]
public class MnCSolveResponse
{
  public Dictionary<string, MnCStage> output;
  public int number_of_states = -1;
}
}
