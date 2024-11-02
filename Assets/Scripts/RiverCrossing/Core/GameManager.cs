using System.Collections.Generic;
using dev.vivekraman.RiverCrossing.Core.Enums;
using dev.vivekraman.RiverCrossing.Core.Rules;
using dev.vivekraman.RiverCrossing.Core.Spawner;
using dev.vivekraman.RiverCrossing.MissionariesAndCannibals;
using UnityEngine;
using UnityEngine.Assertions;

namespace dev.vivekraman.RiverCrossing.Core
{
[RequireComponent(typeof(MissionariesAndCannibalsStateManager))]
[RequireComponent(typeof(RuleEngine))]
[RequireComponent(typeof(InitialSpawner))]
public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; } = null;

  public bool CanBoatMove { get; set; } = true;
  public bool CanBoardBoat { get; set; } = true;

  public Boat TheBoat => boat;
  public MissionariesAndCannibalsStateManager StateManager { get; private set; } = null;
  public RuleEngine TheRuleEngine { get; private set; } = null;
  public InitialSpawner Spawner { get; private set; } = null;

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

    StateManager = this.GetComponent<MissionariesAndCannibalsStateManager>();
    TheRuleEngine = this.GetComponent<RuleEngine>();
    Spawner = this.GetComponent<InitialSpawner>();
    Assert.IsNotNull(StateManager);
    Assert.IsNotNull(TheRuleEngine);
    Assert.IsNotNull(Spawner);

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
