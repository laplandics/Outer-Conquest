using System;
using UnityEngine;

public class SpaceStation : Entity
{
    
}

[Serializable]
public class SpaceStationData : EntityData
{
    public SpaceStationType stationType;
    public SpaceStationTier stationTier;
    public Vector3 position;
    public Quaternion rotation;
}
public enum SpaceStationType { MiningStation, ProductionStation, CitadelStation, EnergyStation, LogisticStation, DockStation, ScienceStation }
public enum SpaceStationTier { Tier1, Tier2, Tier3, Tier4, Tier5, Tier6 }