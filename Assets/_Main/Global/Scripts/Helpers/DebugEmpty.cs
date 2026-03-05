using System;
using System.Collections.Generic;
using UnityEngine;

public class DebugEmpty : MonoBehaviour
{
    public List<Action> GizmosActions = new();
    
    public void Initialize() { gameObject.name = "Debug"; DontDestroyOnLoad(gameObject); }

    private void OnDrawGizmos()
    { if (GizmosActions.Count == 0) return; foreach (var action in GizmosActions) action?.Invoke(); }
}