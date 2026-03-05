using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelectionHandler : IDisposable
{
    private UnitSelector _currentHover;
    private UnitSelector _currentSelected;
    private GameInputs _inputs;
    private Coroutine _hoverCoroutine;
    
    public UnitSelectionHandler()
    {
        _inputs = G.Input.Get;
        _inputs.Units.Enable();
        _inputs.Units.Select.performed += SelectUnit;
        
    }
    
    private void SelectUnit(InputAction.CallbackContext ctx)
    {
        if (_currentHover == _currentSelected) return;
        _currentSelected?.OnDeselect();
        _currentSelected = _currentHover;
        _currentSelected?.OnSelect();
    }

    public void Dispose()
    {
        if (_hoverCoroutine != null) G.Coroutine.Stop(_hoverCoroutine);
        _hoverCoroutine = null;
        _inputs.Units.Select.performed -= SelectUnit;
        _inputs = null;
    }
}