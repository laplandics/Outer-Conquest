using System;
using UnityEngine;

namespace G
{
    public static class Data
    {
        public static UnitData NewUnit(string entityName, string unitSpecific)
        {
            var data = new UnitData
            {
                key = Guid.NewGuid().ToString(),
                entityName = entityName,
                unitSpecification = unitSpecific,
                position = new Vector2(0f, 0f),
                components = Array.Empty<UnitComponentData>()
            };
            return data;
        }
        
        public static CamData NewGameCamera(CamType camType)
        {
            var isMain = camType == CamType.GameplayCam;
            var data = new CamData
            {
                key = Guid.NewGuid().ToString(),
                entityName = camType.ToString(),
                camType = camType,
                isMain = isMain,
            };
            return data;
        }
    }
}