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
    
    public void AddEntityComponent<T>() where T : EntityComponent
    { var newComponent = gameObject.AddComponent<T>(); components.Add(newComponent); newComponent.Initialize(this); }

    public void AddEntityComponent(Type componentType)
    { if (componentType.BaseType != typeof(EntityComponent)) throw new ArgumentException("Invalid component type: " + componentType.FullName);
        var newComponent = gameObject.AddComponent(componentType) as EntityComponent; if (newComponent == null) return;
        components.Add(newComponent); newComponent.Initialize(this); }

    public T GetEntityComponent<T>() where T : EntityComponent
    { foreach (var component in components) { if (component.GetType() == typeof(T)) return (T)component; } return null; }
    
    public EntityComponent GetEntityComponent(Type componentType)
    { foreach (var component in components) { if (component.GetType() == componentType) return component; } return null; }
    
    public T GetData<T>() where T : EntityData
    { if (data is  T entityData) return entityData; return null; }
}

public abstract class EntityComponent : MonoBehaviour
{
    [SerializeField][ReadOnly] protected Entity owner;
    [SerializeField][ReadOnly] protected ComponentPrefs componentPrefs;
    
    public virtual void Initialize(Entity newOwner) {}
    public virtual void Enable() {}
    public virtual void Disable() {}
}

[Serializable]
public abstract class EntityData
{ public EntityType entityType; public List<EntityComponentType> components; }
public enum EntityType { GameCamera, SpaceShip }
public enum EntityComponentType { Controller, RayCaster, Visualiser, Selector }

public abstract class ComponentPrefs : ScriptableObject {}