using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [ReadOnly][SerializeReference] protected EntityData data;
    [ReadOnly][SerializeReference] protected List<EntityComponent> components = new();

    public void Load(EntityData newData)
    { data = newData; foreach (var component in data.components)
    { var typeName = $"{data.entityType.ToString()}{component.ToString()}"; var componentType = Type.GetType(typeName); 
        AddEntityComponent(componentType); } }
    
    public virtual void Enable()
    { foreach (var component in components) component.Enable(); }

    public virtual void Disable()
    { foreach (var component in components) component.Disable(); }
    
    public virtual EntityData GetData() { return data; }
    
    public T GetData<T>() where T : EntityData
    { if (data is  T entityData) return entityData; return null; }
    
    public void AddEntityComponent<T>() where T : EntityComponent
    { var newComponent = gameObject.AddComponent<T>(); components.Add(newComponent); newComponent.Initialize(this); }

    public void AddEntityComponent(Type componentType)
    { if (componentType.BaseType != null && componentType.BaseType.IsAssignableFrom(typeof(EntityComponent)))
            throw new ArgumentException("Invalid component type: " + componentType.FullName);
        var newComponent = gameObject.AddComponent(componentType) as EntityComponent; if (newComponent == null) return;
        components.Add(newComponent); newComponent.Initialize(this); }

    public T GetEntityComponent<T>() where T : EntityComponent
    { foreach (var component in components) { if (component.GetType() == typeof(T) || component.GetType().BaseType == typeof(T)) return (T)component; } return null; }
    
    public EntityComponent GetEntityComponent(Type componentType)
    { foreach (var component in components) { if (component.GetType() == componentType) return component; } return null; }
    
    public List<EntityComponent> GetEntityComponents() => components;
}

public abstract class EntityComponent : MonoBehaviour
{
    [SerializeField][ReadOnly] protected Entity owner;
    
    public virtual void Initialize(Entity newOwner) { owner = newOwner; }
    public virtual void Enable() {}
    public virtual void Disable() {}
    public Entity GetOwner() => owner;
}

public abstract class EntitySelector : EntityComponent
{
    public bool IsSelected { get; protected set; }
    public override void Initialize(Entity newOwner)
    { owner = newOwner; Eventer.Subscribe<EntitiesSelected>(Select); Eventer.Subscribe<EntitiesDeselected>(Deselect); }

    protected virtual void Select(EntitiesSelected eventData) { IsSelected = eventData.SelectedEntities.Contains(this); }
    protected virtual void Deselect(EntitiesDeselected _) { IsSelected = false; }
    public abstract Bounds GetBounds();
}

public abstract class EntitySaveLoader : EntityComponent
{
    protected void LoadComponents<T>() where T : EntityData
    {
        var data = owner.GetData<T>();
        foreach (var component in owner.GetEntityComponents())
        {
            if(component is ILoadingComponent<T> loadingComponent)
                loadingComponent.Load(data);
        }
    }
}

public abstract class EntityUiRenderer : EntityComponent
{
    public abstract GameObject GetSelectionUi();
    public abstract GameObject GetSelectionMemberUi();
}

public abstract class EntityController : EntityComponent {}
public abstract class EntityVisualizer : EntityComponent {}

[Serializable]
public abstract class EntityData
{ public EntityType entityType; public List<EntityComponentType> components; }
public enum EntityType { GameCamera, SpaceShip, SpaceStation }
public enum EntityComponentType { Controller, Visualizer, Selector, SaveLoader, UiRenderer }

public abstract class EntityAsset : MonoBehaviour {}

public interface ILoadingComponent<in T> where T : EntityData { public void Load(T data); }