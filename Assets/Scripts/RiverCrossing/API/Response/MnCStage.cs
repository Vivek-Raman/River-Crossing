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

  public RiverBankSide ParseRiverBankSide()
  {
    foreach (RiverBankSide side in Enum.GetValues(typeof(RiverBankSide)))
    {
      if (side.ToString().ToLower() == boat_position.ToLower())
      {
        return side;
      }
    }

    return RiverBankSide.Null;
  }
}
}
