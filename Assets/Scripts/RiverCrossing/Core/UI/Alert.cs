using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace dev.vivekraman.RiverCrossing.Core.UI
{
public class Alert : MonoBehaviour
{
  private TMP_Text contents = null;

  private void Awake()
  {
    contents = this.GetComponentInChildren<TMP_Text>(true);
    Assert.IsNotNull(contents);
    this.gameObject.SetActive(false);
  }

  public IEnumerator Show(string message)
  {
    if (this.gameObject.activeInHierarchy) yield break;
    this.gameObject.SetActive(true);
    contents.text = message;
    yield return new WaitForSeconds(3);
    contents.text = "";
    this.gameObject.SetActive(false);
  }
}
}
