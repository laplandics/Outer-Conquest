using System.Collections.Generic;
using UnityEngine;

public abstract class Event {}

public class ZoomChanged : Event { public int Zoom; }

public class UnitCreateBegin : Event { public UnitData Data; public List<Vector2> Positions; }
public class UnitCreateEnd : Event { public Unit Unit; public Vector2 Position; }