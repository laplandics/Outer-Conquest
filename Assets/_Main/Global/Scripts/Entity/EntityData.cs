using System;

[Serializable]
public abstract class EntityData
{
    [ReadOnly] public string key;
    [ReadOnly] public string entityName;
}