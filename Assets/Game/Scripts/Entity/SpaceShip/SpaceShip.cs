using System;
using UnityEngine;

public class SpaceShip : Entity
{
    
}

[Serializable]
public class SpaceShipData : EntityData
{
    public SpaceShipType spaceShipType;
    public SpaceShipClass spaceShipClass;
    public Vector3 position;
    public Quaternion rotation;
}
public enum SpaceShipType { BattleShip, BuildShip, ScienceShip, CargoShip, CitizenShip, MotherShip }
public enum SpaceShipClass { Mk0, Mk1, Mk2, Mk3, Mk4 }