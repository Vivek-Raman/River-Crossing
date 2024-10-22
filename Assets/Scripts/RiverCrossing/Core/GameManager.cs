using System.Collections.Generic;
using dev.vivekraman.RiverCrossing.Core.Enums;
using dev.vivekraman.RiverCrossing.MissionariesAndCannibals;
using UnityEngine;
using UnityEngine.Assertions;

namespace dev.vivekraman.RiverCrossing.Core
{
public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; } = null;

  public bool BoatCanMove { get; set; } = true;
  public bool CanBoardBoat { get; set; } = true;

  public Boat TheBoat => boat;
  [SerializeField] private Boat boat = null;

  public MissionariesAndCannibalsStateManager StateManager { get; private set; } = null;

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
    Assert.IsNotNull(StateManager);

    Assert.IsNotNull(boat);
  }

  private void Start()
  {
    Assert.IsTrue(riverBanks.Count == 2);
  }

  [ContextMenu(nameof(DescribeState))]
  public void DescribeState()
  {
    Debug.LogFormat("State: \nBoatCanMove: ${0}\n CanBoardBoat: ${1}", BoatCanMove, CanBoardBoat);
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
