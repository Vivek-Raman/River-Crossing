using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Assertions;

namespace dev.vivekraman.RiverCrossing.Core.Cameras
{
public class CameraController : MonoBehaviour
{
  [SerializeField] private CinemachineCamera mainMenuCamera = null;
  [SerializeField] private CinemachineCamera gameplayCamera = null;

  private void Awake()
  {
    Assert.IsNotNull(mainMenuCamera);
    Assert.IsNotNull(gameplayCamera);
  }

  public void FocusMainMenu()
  {
    mainMenuCamera.Prioritize();
  }

  public void FocusGameplay()
  {
    gameplayCamera.Prioritize();
  }
}
}
