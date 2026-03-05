using System;
using UnityEngine;

public class BuildUi : GameUi
{
    [Serializable] public class StationBuilds { public string name; public bool build; }
    
    public StationBuilds[] stationBuilds;
    private float _timer;
    
    public override void Appear(IUiUser user)
    {
        base.Appear(user);
        var stations = Enum.GetNames(typeof(StationType));
        stationBuilds = new StationBuilds[stations.Length];
        for (var i = 0; i < stationBuilds.Length; i++)
        { stationBuilds[i] = new StationBuilds { name = stations[i], build = false }; }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < 0.2f) return;
        CheckStations();
        _timer = 0f;
    }

    private void CheckStations()
    {
        if (stationBuilds == null || stationBuilds.Length == 0) return;
        foreach (var station in stationBuilds)
        { if (!station.build) continue; station.build = false;
            BuildUnit(StationConfig.UNIT_TYPE.ToString(), station.name); break; }
    }

    private void BuildUnit(string unitType, string unitSpecific)
    {
        var data = G.Data.NewUnit(unitType, unitSpecific);
        UiActions[nameof(BuildUnit)].Invoke(data);
    }
}