using dev.vivekraman.RiverCrossing.Core.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace dev.vivekraman.RiverCrossing.Core.UI
{
public class ConfigController : MonoBehaviour
{
  [SerializeField] private TMP_Text boatCapacityMnCText = null;
  [SerializeField] private TMP_Text characterCountMnCText = null;
  [SerializeField] private TMP_Text boatCapacityJHText = null;
  [SerializeField] private TMP_Text characterCountJHText = null;
  [SerializeField] private Slider boatCapacityMnCSlider = null;
  [SerializeField] private Slider characterCountMnCSlider = null;
  [SerializeField] private Slider boatCapacityJHSlider = null;
  [SerializeField] private Slider characterCountJHSlider = null;

  private void Awake()
  {
    Assert.IsNotNull(boatCapacityMnCText);
    Assert.IsNotNull(characterCountMnCText);
    Assert.IsNotNull(boatCapacityJHText);
    Assert.IsNotNull(characterCountJHText);
    Assert.IsNotNull(boatCapacityMnCSlider);
    Assert.IsNotNull(characterCountMnCSlider);
    Assert.IsNotNull(boatCapacityJHSlider);
    Assert.IsNotNull(characterCountJHSlider);
  }

  private void Start()
  {
    UI_BoatCapacityChanged(2);
    UI_CharacterCountChanged(3);
    UI_TraversalMethodChanged(0);
  }

  public void UI_BoatCapacityChanged(float value)
  {
    GameManager gameManager = GameManager.Instance;
    gameManager.BoatCapacity = Mathf.RoundToInt(value);
    switch (gameManager.TheRuleEngine.TheGameMode)
    {
      case GameMode.MissionariesAndCannibals:
        boatCapacityMnCText.text = boatCapacityMnCText.text.Remove(boatCapacityMnCText.text.Length - 1) +
                                   GameManager.Instance.BoatCapacity;
        break;
      case GameMode.JealousHusbands:
        boatCapacityJHText.text = boatCapacityJHText.text.Remove(boatCapacityJHText.text.Length - 1) +
                                  GameManager.Instance.BoatCapacity;
        break;
    }
  }

  public void UI_CharacterCountChanged(float value)
  {
    GameManager gameManager = GameManager.Instance;
    gameManager.CharacterCount = Mathf.RoundToInt(value);
    switch (gameManager.TheRuleEngine.TheGameMode)
    {
      case GameMode.MissionariesAndCannibals:
        characterCountMnCText.text = characterCountMnCText.text.Remove(characterCountMnCText.text.Length - 1) +
                                     GameManager.Instance.CharacterCount;
        break;
      case GameMode.JealousHusbands:
        characterCountJHText.text = characterCountJHText.text.Remove(characterCountJHText.text.Length - 1) +
                                    GameManager.Instance.CharacterCount;
        break;
    }
    gameManager.Spawner.LoadInitialStateForGameMode(gameManager.TheRuleEngine.TheGameMode);
  }

  public void UI_TraversalMethodChanged(int value)
  {
    GameManager gameManager = GameManager.Instance;
    gameManager.Solver.Traversal = (TraversalMode) value;
  }

  public void ForceResetSliders()
  {
    boatCapacityMnCSlider.value = 2;
    characterCountMnCSlider.value = 3;
    boatCapacityJHSlider.value = 2;
    characterCountJHSlider.value = 3;
  }
}
}
