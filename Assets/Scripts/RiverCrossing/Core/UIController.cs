using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace dev.vivekraman.RiverCrossing.Core
{
public class UIController : MonoBehaviour
{
  [SerializeField] private GameObject mainMenuPanel = null;

  private void Awake()
  {
    Assert.IsNotNull(mainMenuPanel);
  }

  public void SetMainMenuUIState(bool visible)
  {
    mainMenuPanel.SetActive(visible);
  }

  // TODO: set up button handlers
}
}
