using System;
using System.Collections.Generic;
using UnityEngine;

public static class E
{
    public static T NewEntity<T>(EntityData data) where T : Entity
    {
        var newEntity = new GameObject(data.entityType.ToString()).AddComponent<T>();
        newEntity.Load(data);
        newEntity.Enable();
        Eventer.Invoke(new EntitySpawned { SpawnedEntity = newEntity });
        return newEntity;
    }

    public static Entity NewEntity(EntityData data)
    {
        var entityTypeName = data.entityType.ToString();
        var type = Type.GetType(entityTypeName);
        var newEntity = new GameObject(entityTypeName).AddComponent(type) as Entity;
        if (newEntity == null) throw new Exception($"Could not find type {entityTypeName} for new entity");
        newEntity.Load(data);
        newEntity.Enable();
        Eventer.Invoke(new EntitySpawned { SpawnedEntity = newEntity });
        return newEntity;
    }
    
    public static void DeleteEntity(Entity entity)
    {
        if (entity == null) return;
        Eventer.Invoke(new EntityDespawned { DespawnedEntity = entity });
        entity.Disable();
        Spawner.Despawn(entity.gameObject);
    }
}