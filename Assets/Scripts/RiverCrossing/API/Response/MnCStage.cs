using System;
using dev.vivekraman.RiverCrossing.Core.Enums;

namespace dev.vivekraman.RiverCrossing.API.Response
{
[System.Serializable]
public class MnCStage
{
  public int M_left  = 0;
  public int C_left  = 0;
  public int M_right = 0;
  public int C_right = 0;
  public string boat_position = "";

  public override string ToString()
  {
    return "[Boat: " + boat_position + "\tM_left: " + M_left + ", C_left: " + C_left + ", M_right: " + M_right + ", C_right: " + C_right + "]";
  }
}
}
