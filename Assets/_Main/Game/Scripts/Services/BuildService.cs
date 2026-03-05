using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "BuildingService", menuName = "Service/Building")]
public class BuildService : Service, IUiUser
{
    private GameInputs _inputs;
    private BuildUi _ui;
    private bool _isBuilding;
    
    public override void ServiceAwake()
    {
        _inputs = G.Input.Get;
        _inputs.Menu.Build.Enable();
        _inputs.Menu.Build.performed += ToggleBuildMenu;
    }

    public void BuildUnit(object param)
    {
        if (param is not UnitData unitData) throw new Exception("Build parameter is not a UnitData");
        G.Coroutine.Start(Build(unitData));
    }

    private IEnumerator Build(UnitData unitData)
    {
        var cam = Camera.main;
        if (cam == null) { Debug.LogError("Camera is null"); yield break; }
        var mouse = Mouse.current;
        G.Grid.ForAllTilesInGrid(DrawGrid);
        while (_isBuilding)
        {
            if (Mouse.current.leftButton.isPressed)
            {
                var windowPos = mouse.position.ReadValue();
                var worldPos = cam.ScreenToWorldPoint(windowPos);
                var vector2WorldPos = new Vector2(worldPos.x, worldPos.y);
                var tile = G.Grid.GetTile(vector2WorldPos);
                var tileCenter = G.Grid.GetCenter(tile);
                unitData.position = tileCenter;
                var entity = new Entity<Unit>(unitData);
                yield break;
            }
            yield return null;
        }
        
        yield break;
        void DrawGrid(Vector2Int tile)
        {
            var center = G.Grid.GetCenter(tile);
            Debug.Log(center);
        }
    }
    
    private void ToggleBuildMenu(InputAction.CallbackContext ctx)
    { if (!_isBuilding) ShowUi(); else { HideUi(); } }
    public void ShowUi() { _ui = G.Ui.Create<BuildUi>(this); _isBuilding = true; }
    public void HideUi() { G.Ui.Delete(_ui); _isBuilding = false; }
    public (string, Action<object>)[] GetActions()
    { var actions = new (string, Action<object>)[] { (nameof(BuildUnit), BuildUnit) }; return actions; }
    
    public override void ServiceStop()
    {
        _ui = null;
        _isBuilding = false;
        _inputs.Menu.Build.performed -= ToggleBuildMenu;
        _inputs.Menu.Build.Disable();
        _inputs = null;
    }
}