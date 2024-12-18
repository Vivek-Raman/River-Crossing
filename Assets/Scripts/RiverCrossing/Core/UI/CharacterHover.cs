using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace dev.vivekraman.RiverCrossing.Core.UI
{
public class CharacterHover : MonoBehaviour
{
  public bool Initialized { get; set; } = false;

  [SerializeField] private GameObject hoverCanvas = null;

  private TMP_Text tooltipText = null;
  private Camera mainCamera = null;
  private Character trackedCharacter = null;

  private void Awake()
  {
    Assert.IsNotNull(hoverCanvas);

    tooltipText = hoverCanvas.GetComponentInChildren<TMP_Text>();
    Assert.IsNotNull(tooltipText);
    hoverCanvas.SetActive(false);

    mainCamera = Camera.main;
    Assert.IsNotNull(mainCamera);
  }

  private void LateUpdate()
  {
    if (!Initialized) return;
    if (trackedCharacter == null) return;

    hoverCanvas.transform.position = trackedCharacter.transform.position + new Vector3(0f, 2.5f, 0f);
    hoverCanvas.transform.forward = mainCamera.transform.forward;
  }

  public void OnHoverEnter(Character character)
  {
    if (!Initialized) return;
    trackedCharacter = character;
    tooltipText.text = trackedCharacter.DisplayName;
    hoverCanvas.SetActive(true);
  }

  public void OnHoverExit(Character character)
  {
    if (!Initialized) return;
    trackedCharacter = null;
    tooltipText.text = string.Empty;
    hoverCanvas.SetActive(false);
  }
}
}
