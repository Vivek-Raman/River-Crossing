using System.Collections.Generic;
using dev.vivekraman.RiverCrossing.Core.Cameras;
using dev.vivekraman.RiverCrossing.Core.Enums;
using dev.vivekraman.RiverCrossing.Core.Rules;
using dev.vivekraman.RiverCrossing.Core.Score;
using dev.vivekraman.RiverCrossing.Core.Solver;
using dev.vivekraman.RiverCrossing.Core.Spawner;
using dev.vivekraman.RiverCrossing.Core.States;
using UnityEngine;
using UnityEngine.Assertions;

namespace dev.vivekraman.RiverCrossing.Core
{
[RequireComponent(typeof(RiverCrossingStateManager))]
[RequireComponent(typeof(RuleEngine))]
[RequireComponent(typeof(InitialSpawner))]
[RequireComponent(typeof(CameraController))]
[RequireComponent(typeof(UIController))]
[RequireComponent(typeof(ScoreController))]
[RequireComponent(typeof(GameSolver))]
public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; } = null;

  public bool CanBoatMove { get; set; } = true;
  public bool CanBoardBoat { get; set; } = true;

  public Boat TheBoat => boat;
  public RiverCrossingStateManager StateManager { get; private set; } = null;
  public RuleEngine TheRuleEngine { get; private set; } = null;
  public InitialSpawner Spawner { get; private set; } = null;
  public CameraController TheCameraController { get; private set; } = null;
  public UIController TheUIController { get; private set; } = null;
  public ScoreController TheScoreController { get; private set; } = null;
  public GameSolver Solver { get; private set; } = null;

  [SerializeField] private Boat boat = null;

  private Dictionary<RiverBankSide, RiverBank> riverBanks = new();

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }

    StateManager = this.GetComponent<RiverCrossingStateManager>();
    TheRuleEngine = this.GetComponent<RuleEngine>();
    Spawner = this.GetComponent<InitialSpawner>();
    TheCameraController = this.GetComponent<CameraController>();
    TheUIController = this.GetComponent<UIController>();
    TheScoreController = this.GetComponent<ScoreController>();
    Solver = this.GetComponent<GameSolver>();

    Assert.IsNotNull(StateManager);
    Assert.IsNotNull(TheRuleEngine);
    Assert.IsNotNull(Spawner);
    Assert.IsNotNull(TheCameraController);
    Assert.IsNotNull(TheUIController);
    Assert.IsNotNull(TheScoreController);
    Assert.IsNotNull(Solver);

    Assert.IsNotNull(boat);
  }

  public void RegisterRiverBank(RiverBank riverBank)
  {
    riverBanks[riverBank.Side] = riverBank;
  }

  public RiverBank GetRiverBank(RiverBankSide side)
  {
    return riverBanks[side];
  }
}
}
