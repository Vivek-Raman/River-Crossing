using dev.vivekraman.RiverCrossing.Core.Enums;
using UnityEngine;
using UnityEngine.Assertions;

namespace dev.vivekraman.RiverCrossing.Core
{
public class RiverBank : MonoBehaviour
{
  public RiverBankSide Side => side;

  [SerializeField] private RiverBankSide side = RiverBankSide.Left;

  private Transform boatDockingPoint = null;

  private void Awake()
  {
    boatDockingPoint = this.transform.GetChild(0)?.transform;
    Assert.IsNotNull(boatDockingPoint);
  }

  private void Start()
  {
    GameManager.Instance.RegisterRiverBank(this);
  }

  public Transform GetCharacterAnchor()
  {
    // TODO: distribute positions on river bank
    return this.transform;
  }
}
}
