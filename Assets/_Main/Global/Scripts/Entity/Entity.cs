using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Entity<T> where T : IEntity
{
    public string Key {get; private set;}
    public T Instance {get; private set;}
    private IEntity _entity;

    public Entity(EntityData data)
    {
        Key = data.key;
        var entityType = Type.GetType(data.entityName);
        if (entityType == null) throw new Exception("Entity not found: " + data.entityName);
        _entity = new GameObject(data.entityName).AddComponent(entityType) as IEntity;
        if (_entity == null) throw new Exception("Entity could not be created: " + data.entityName);
        _entity.Create(data);
        Instance = (T)_entity;
    }

    public void Destroy()
    {
        _entity.Destruct();
        Object.Destroy(_entity.Instance);
        _entity = null;
    }
}