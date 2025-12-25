using System;
using UnityEngine;

public static class E
{
    public static void NewEntity<T>(EntityData data, Action<T> onLoad) where T : Entity
    {
        var newObject = new GameObject(data.entityType.ToString()).AddComponent<T>();
        newObject.Load(data);
    }

    public static void NewEntity(EntityData data, Action<Entity> onLoad)
    {
        var entityTypeName = data.entityType.ToString();
        var type = Type.GetType(entityTypeName);
        var newObject = new GameObject(entityTypeName).AddComponent(type) as Entity;
        if (newObject == null) throw new Exception($"Could not find type {entityTypeName} for new entity");
        newObject.Load(data);
    }

    public static void DeleteEntity(Entity entity)
    {
        entity.Disable();
        Spawner.Despawn(entity.gameObject);
    }
}