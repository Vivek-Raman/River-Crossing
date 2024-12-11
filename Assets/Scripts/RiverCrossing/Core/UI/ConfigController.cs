using dev.vivekraman.RiverCrossing.Core.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace dev.vivekraman.RiverCrossing.Core.UI
{
public class ConfigController : MonoBehaviour
{
  [SerializeField] private TMP_Text boatCapacityMnCText = null;
  [SerializeField] private TMP_Text characterCountMnCText = null;
  [SerializeField] private TMP_Text boatCapacityJHText = null;
  [SerializeField] private TMP_Text characterCountJHText = null;

  private void Awake()
  {
    Assert.IsNotNull(boatCapacityMnCText);
    Assert.IsNotNull(characterCountMnCText);
    Assert.IsNotNull(boatCapacityJHText);
    Assert.IsNotNull(characterCountJHText);
  }

  private void Start()
  {
    UI_BoatCapacityChanged(2);
    UI_CharacterCountChanged(3);
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
}
}
