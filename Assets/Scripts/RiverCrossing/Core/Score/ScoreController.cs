using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace dev.vivekraman.RiverCrossing.Core.Score
{
public class ScoreController : MonoBehaviour
{
  [SerializeField] private TMP_Text scoreText;

  private int score = 0;

  private void Awake()
  {
    Assert.IsNotNull(scoreText);
  }

  private void Start()
  {
    UpdateScoreDisplay();
  }

  public void AddScore(int toAdd = 1)
  {
    score += toAdd;
    UpdateScoreDisplay();
  }

  private void UpdateScoreDisplay()
  {
    scoreText.text = "Score: " + score.ToString();
  }
}
}
