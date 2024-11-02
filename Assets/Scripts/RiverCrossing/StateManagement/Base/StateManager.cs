using System.Collections.Generic;
using UnityEngine;

namespace dev.vivekraman.RiverCrossing.StateManagement.Base
{
public abstract class StateManager : MonoBehaviour
{
  private Dictionary<string, State> states = new Dictionary<string, State>();
  private State currentState = null;

  protected void RegisterState(State state)
  {
    states[state.Name] = state;
  }

  public void SetState(string stateName)
  {
    if (currentState?.Name == stateName) return;

    if (!states.TryGetValue(stateName, out State newState))
    {
      Debug.LogError("State " + stateName + "not registered");
      return;
    }

    if (currentState != null)
    {
      // Debug.Log("Leaving state: " + currentState.Name);
      StartCoroutine(currentState.OnStateExit());
    }

    currentState = newState;
    // Debug.Log("Entering state: " + currentState.Name);
    StartCoroutine(currentState.OnStateEnter());
  }

  private void Update()
  {
    currentState?.OnStateTick();
  }
}
}
