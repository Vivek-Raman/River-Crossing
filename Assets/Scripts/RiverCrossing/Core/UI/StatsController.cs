using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace dev.vivekraman.RiverCrossing.Core.UI
{
public class StatsController : MonoBehaviour
{
  [SerializeField] private TMP_Text statsText;

  private void Awake()
  {
    Assert.IsNotNull(statsText);
    statsText.text = "";
  }

  public void UpdateStats()
  {
    statsText.text = "Total states: " + GameManager.Instance.Solver.StateCount.ToString();
  }
}
}
