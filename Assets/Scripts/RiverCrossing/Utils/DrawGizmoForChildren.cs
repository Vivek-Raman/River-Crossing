using UnityEngine;

namespace dev.vivekraman.RiverCrossing.Utils
{
[ExecuteInEditMode]
public class DrawGizmoForChildren : MonoBehaviour
{
  private void OnDrawGizmosSelected()
  {
    foreach (Transform child in this.transform)
    {
      Gizmos.DrawSphere(child.transform.position, 0.1f);
    }
  }
}
}
