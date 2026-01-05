using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntitySelectionManager : SceneManager, IStartListener
{
    private GameInputs _inputs;
    private Coroutine _selectionRoutine;
    private bool _isDragging;

    private EmptySpotTargeted _spotTargeted;
    private EntitiesSelected _selectEvent;
    private EntitiesDeselected _deselectEvent;

    public void OnStart()
    {
        _selectEvent = new EntitiesSelected();
        _deselectEvent = new EntitiesDeselected();
        _spotTargeted = new EmptySpotTargeted();
        _inputs = G.GetService<InputService>().GetGameInputs();
        _inputs.Entity.Enable();
        _inputs.Entity.LeftClick.performed += Select;
    }
    
    private void Select(InputAction.CallbackContext input)
    {
        if (_selectionRoutine != null) G.GetManager<RoutineManager>().EndRoutine(_selectionRoutine);
        _selectionRoutine = G.GetManager<RoutineManager>().StartRoutine(SelectionRoutine(input));
    }

    private IEnumerator SelectionRoutine(InputAction.CallbackContext input)
    {
        _isDragging = false;
        var screenEndPos = Vector2.zero;
        if (RayCaster.IsMouseOverUi(out _)) yield break;
        var mouseOutOfPlane = !RayCaster.GetMousePosition(out var worldStartPos, out var screenStartPos);
        while (input.performed)
        {
            RayCaster.GetMousePosition(out _, out screenEndPos); 
            if (Vector2.Distance(screenEndPos, screenStartPos) > 10f) _isDragging = true;
            yield return null;
        }
        if (_isDragging) { SelectMany(screenStartPos, screenEndPos); yield break; }
        if (mouseOutOfPlane) yield break;
        if (IsSelectingEntity(out var selector)) SelectOne(selector);
        _spotTargeted.Spot = worldStartPos;
        Eventer.Invoke(_spotTargeted);
    }

    private bool IsSelectingEntity(out EntitySelector selector)
    { selector = null; return RayCaster.GetMouseHit(out var hit) && hit.collider.TryGetComponent(out selector); }

    private void SelectOne(EntitySelector selector)
    { _selectEvent.SelectedEntities = new List<EntitySelector> { selector }; Eventer.Invoke(_selectEvent); }

    private void SelectMany(Vector2 startMouseScreenPos, Vector2 endMouseScreenPos)
    {
        var selected = new List<EntitySelector>();
        var points = RayCaster.GetFrustumPoints(startMouseScreenPos, endMouseScreenPos);
        var planes = NavMeshHelper.BuildPlanes(points);
        G.GetState<SceneEntities>().GetEntityByComponent<EntitySelector>(out var selectors);
        foreach (var selector in selectors)
        { if (GeometryUtility.TestPlanesAABB(planes, selector.GetBounds())) selected.Add(selector); }
        if(selected.Count == 0) { Eventer.Invoke(_deselectEvent); return; }
        _selectEvent.SelectedEntities = new List<EntitySelector>(selected);
        Eventer.Invoke(_selectEvent);
    }
    
    public override void Deinitialize()
    {
        if (_selectionRoutine != null) G.GetManager<RoutineManager>().EndRoutine(_selectionRoutine);
        _inputs.Entity.LeftClick.performed -= Select;
    }
}