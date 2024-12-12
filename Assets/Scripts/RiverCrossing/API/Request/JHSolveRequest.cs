using dev.vivekraman.RiverCrossing.API.Response;
using dev.vivekraman.RiverCrossing.Core.Enums;

namespace dev.vivekraman.RiverCrossing.API.Request
{
[System.Serializable]
public class JHSolveRequest
{
  public JHStageRaw stage = new JHStageRaw();
  public int num_of_couples = 3;
  public int boat_capacity = 2;
  public string solver = "";

  public void SetSolver(TraversalMode mode)
  {
    switch (mode)
    {
      case TraversalMode.Null:
        break;
      case TraversalMode.BreadthFirst:
        this.solver = "bfs";
        break;
      case TraversalMode.DepthFirst:
        this.solver = "dfs";
        break;
      case TraversalMode.AStar:
        this.solver = "a_star";
        break;
    }
  }
}
}
