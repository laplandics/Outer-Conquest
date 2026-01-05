using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : SceneManager
{
    [SerializeField] private Canvas canvasPrefab;
    [SerializeField] private RectTransform selectionsUiPrefab;
    private RectTransform _selectionsContainer;
    private RectTransform _selectionsUi;
    private Canvas _canvas;
    private Dictionary<Entity,GameObject> _entitySelections = new();

    public override IEnumerator Initialize()
    {
        _canvas = Spawner.Spawn(canvasPrefab, Vector3.zero, Quaternion.identity);
        _canvas.gameObject.name = "GlobalUi";
        _selectionsUi = Spawner.Spawn(selectionsUiPrefab, Vector3.zero, Quaternion.identity);
        _selectionsUi.SetParent(_canvas.transform, false);
        _selectionsContainer = _selectionsUi.GetComponentInChildren<SelectionContainer>().ContainerTransform;
        _selectionsContainer.gameObject.SetActive(false);
        Eventer.Subscribe<EntitiesSelected>(ShowEntitiesUi);
        Eventer.Subscribe<EntitiesDeselected>(HideUi);
        yield break;
    }

    private void ShowEntitiesUi(EntitiesSelected eventData)
    {
        if (eventData.SelectedEntities.Count == 0) return;
        var entitiesToDelete = new List<(Entity entity, GameObject ui)>();
        foreach (var (e, u) in _entitySelections) entitiesToDelete.Add((e, u));
        _entitySelections.Clear();
        foreach (var (_, ui) in entitiesToDelete) { Destroy(ui); }
        foreach (var selector in eventData.SelectedEntities)
        {
            var entity = selector.GetOwner();
            var uiRenderer = entity.GetEntityComponent<EntityUiRenderer>();
            if (uiRenderer == null) return;
            var uiPrefab = eventData.SelectedEntities.Count > 1
                ? uiRenderer.GetSelectionMemberUi()
                : uiRenderer.GetSelectionUi();
            var prefabInstance = Instantiate(uiPrefab, _selectionsContainer, false);
            _entitySelections.Add(entity, prefabInstance);
        }
        _selectionsContainer.gameObject.SetActive(_entitySelections.Count != 0);
    }

    private void HideUi(EntitiesDeselected eventData)
    {
        var uiToDelete = new List<GameObject>(_entitySelections.Values);
        _entitySelections.Clear();
        foreach (var ui in uiToDelete) Destroy(ui);
        _selectionsContainer.gameObject.SetActive(false);
    }

    public override void Deinitialize()
    {
        Eventer.Unsubscribe<EntitiesSelected>(ShowEntitiesUi);
        Eventer.Unsubscribe<EntitiesDeselected>(HideUi);
        Spawner.Despawn(_canvas.gameObject);
    }
}