using System;
using dev.vivekraman.RiverCrossing.Core.Enums;

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
  public int boat_capacity;
  public string solver;

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
