using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace dev.vivekraman.RiverCrossing.Core.Score
{
public class ScoreController : MonoBehaviour
{
  [SerializeField] private TMP_Text scoreText;
  [SerializeField] private TMP_Text targetText;

  private int score = 0;

  private void Awake()
  {
    Assert.IsNotNull(scoreText);
    Assert.IsNotNull(targetText);
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

  public void UpdateTargetDisplay()
  {
    targetText.text = "Target: " + GameManager.Instance.Solver.GetBestSolutionStepCount().ToString();
  }

  private void UpdateScoreDisplay()
  {
    scoreText.text = "Score: " + score.ToString();
  }
}
}
