using System;
using UnityEngine;

[Serializable]
public class UnitData : EntityData
{
    [ReadOnly] public string unitSpecification;
    [ReadOnly] public UnitComponentData[] components;
    public Vector2 position;
}

[Serializable]
public class UnitComponentData
{
    public string type;
    public string name;
    public string unitSpot;
}