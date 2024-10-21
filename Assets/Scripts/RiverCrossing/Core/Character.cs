using UnityEngine;

namespace dev.vivekraman.RiverCrossing.Core
{
public class Character : MonoBehaviour
{
  [SerializeField] private string characterClass;
  
  private RiverBank nearestBank = null;

  private void Awake()
  {
    // TODO: determine nearestBank
  }
}
}
