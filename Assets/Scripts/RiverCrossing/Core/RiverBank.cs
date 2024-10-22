using System;
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
    GameManager.Instance.RegisterRiverBank(this);
  }
}
}
