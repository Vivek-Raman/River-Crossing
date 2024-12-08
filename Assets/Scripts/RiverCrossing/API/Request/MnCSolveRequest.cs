namespace dev.vivekraman.RiverCrossing.API.Request
{
[System.Serializable]
public class MnCSolveRequest
{
  public int M_total;
  public int C_total;
  public int M_left;
  public int C_left;
  public int M_right;
  public int C_right;
  public string boat_position;
  public string solver;
}
}
