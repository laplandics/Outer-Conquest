using System;
using System.Collections.Generic;
using UnityEngine;

public class Empty : MonoBehaviour
{
    public enum UpdateType { Update, LateUpdate }
    
    public List<Action> GizmosActions = new();
    private List<Action> _updateActions = new();
    private List<Action> _lateUpdateActions = new();
    
    public void Initialize()
    { gameObject.name = "Empty"; DontDestroyOnLoad(gameObject); }

    public void SetNewUpdate(Action action, UpdateType type)
    {
        switch (type)
        {
            case UpdateType.Update: _updateActions.Add(action); break;
            case UpdateType.LateUpdate: _lateUpdateActions.Add(action); break;
        }
    }

    public void RemoveUpdate(Action action)
    {
        if (_updateActions.Contains(action)) _updateActions.Remove(action);
        else if (_lateUpdateActions.Contains(action)) _lateUpdateActions.Remove(action);
    }
    
    private void Update()
    { if (_updateActions.Count == 0) return; foreach (var action in _updateActions) action?.Invoke(); }

    private void LateUpdate()
    { if (_lateUpdateActions.Count == 0) return; foreach (var action in _lateUpdateActions) action?.Invoke(); }
    
    private void OnDrawGizmos()
    { if (GizmosActions.Count == 0) return; foreach (var action in GizmosActions) action?.Invoke(); }
}