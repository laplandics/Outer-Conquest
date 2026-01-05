using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpaceShipPrefs", menuName = "Preferences/SpaceShipPrefs")]
public class SpaceShipPrefs : Preferences
{
    [SerializeField] private List<TypeClassPrefs> preferences = new();
    private Dictionary<string, TypeClassPrefs> _preferencesDict = new();

    private void OnEnable()
    {
        _preferencesDict = new Dictionary<string, TypeClassPrefs>();
        foreach (var prefs in preferences)
        { var key = $"{prefs.spaceShipType}{prefs.spaceShipClass}"; _preferencesDict.Add(key, prefs); }
    }

    public TypeClassPrefs GetPreferences(string shipKey) => _preferencesDict[shipKey];
}

[Serializable]
public class TypeClassPrefs
{
    public SpaceShipType spaceShipType;
    public SpaceShipClass spaceShipClass;
    public float baseMoveSpeed;
    public float baseRotateSpeed;
}