using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneEntities", menuName = "States/SceneEntities")]
public class SceneEntities : GameState
{
    private HashSet<Entity> _entities;
    
    public override IEnumerator Load()
    {
        _entities = new HashSet<Entity>();
        Eventer.Subscribe<EntitySpawned>(AddEntity);
        Eventer.Subscribe<EntityDespawned>(RemoveEntity);
        yield break;
    }

    private void AddEntity(EntitySpawned eventData) => _entities.Add(eventData.SpawnedEntity);
    private void RemoveEntity(EntityDespawned eventData) => _entities.Remove(eventData.DespawnedEntity);
    
    public List<Entity> GetAllEntities() => new(_entities);

    public List<Entity> GetEntityByComponent<T>(out List<T> components) where T : EntityComponent
    {
        var result = new List<Entity>();
        var result2 = new List<T>();
        foreach (var entity in _entities)
        { foreach (var entityComponent in entity.GetEntityComponents())
            { if (entityComponent is not T component) continue; result.Add(entity); result2.Add(component); }
        }
        components = result2;
        return result;
    }

    public override void Unload()
    {
        Eventer.Unsubscribe<EntitySpawned>(AddEntity);
        Eventer.Unsubscribe<EntityDespawned>(RemoveEntity);
    }
}