using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IEntity
{
    [ReadOnly][SerializeReference] protected UnitData unitData;
    [ReadOnly][SerializeField] protected ScriptableObject unitConfig;
    [ReadOnly][SerializeReference] protected List<UnitSystem> systems;
    [ReadOnly][SerializeField] protected List<UnitComponent> components;

    public GameObject Instance => gameObject;

    public void Create(EntityData data)
    { if (data is not UnitData newData) throw new Exception("Entity data is not a UnitData");
        unitData = newData; components = new List<UnitComponent>(); systems = new List<UnitSystem>();
        unitConfig = G.Load.LoadConfig(unitData.entityName); OnCreate(); }

    public abstract void OnCreate();

    protected void StartSystems() { foreach (var system in systems) system.Initialize(this); }

    protected void EndSystems() { foreach (var system in systems) system.Deinitialize(); }

    public void Destruct()
    { OnDestruct(); DeleteComponents(); Resources.UnloadUnusedAssets(); }

    public abstract void OnDestruct();

    public UnitData GetData() => unitData;

    public T GetConfig<T>() where T : ScriptableObject
    { if (unitConfig is T config) return config; return null; }

    protected void CreateSystem<T>() where T : UnitSystem, new()
    { var system = Activator.CreateInstance<T>(); systems.Add(system); system.systemName = typeof(T).Name; }

    protected void DeleteSystem<T>() where T : UnitSystem
    { var systemToRemove = systems.Find(x => x.GetType() == typeof(T)); systems.Remove(systemToRemove); }

    public T FindSystem<T>() where T : UnitSystem
    { foreach (var system in systems) { if (system is T unitSystem) return unitSystem; } return null; }

    public T CreateComponent<T>(GameObject componentObj) where T : UnitComponent
    { var component = componentObj.AddComponent<T>(); components.Add(component);
        component.Initialize(this); return component; }

    public void DeleteComponent<T>() where T : UnitComponent
    { var componentToRemove = components.Find(x => x.GetType() == typeof(T)); componentToRemove.Deinitialize();
        components.Remove(componentToRemove); }

    private void DeleteComponents() 
    { foreach (var component in components) { component.Deinitialize(); } components.Clear(); }

    public UnitComponent CreateComponent(Type componentType, GameObject componentObj)
    { var component = (UnitComponent)componentObj.AddComponent(componentType); components.Add(component);
        component.Initialize(this); return component; }

    public void DeleteComponent(Type componentType)
    { var componentToRemove = components.Find(x => x.GetType() == componentType); componentToRemove.Deinitialize();
        components.Remove(componentToRemove); }

    public T FindComponent<T>() where T : UnitComponent
    { foreach (var component in components) { if (component is T unitComponent) return unitComponent; } return null; }

    public UnitComponent[] GetAllComponents() => components.ToArray();
}