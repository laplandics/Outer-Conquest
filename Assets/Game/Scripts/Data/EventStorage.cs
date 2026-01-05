using System.Collections.Generic;
using UnityEngine;

public abstract class Event {}

public class StatesLoaded : Event {}
public class StatesUnloaded : Event {}

public class ServicesLaunched : Event {}
public class ServicesStopped : Event {}

public class ManagersInitialized : Event {}
public class ManagersDeinitialized : Event {}

public class SceneStarted : Event {}
public class SceneEnded : Event {}

public class EntitySpawned : Event { public Entity SpawnedEntity;  }
public class EntityDespawned : Event { public Entity DespawnedEntity; }

public class EntitiesSelected : Event { public List<EntitySelector> SelectedEntities = new(); }
public class EntitiesDeselected : Event { }

public class EmptySpotTargeted : Event { public Vector3 Spot; }