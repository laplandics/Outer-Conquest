using UnityEngine;

[CreateAssetMenu(fileName = "GlobalPrefs", menuName = "Settings/GlobalPrefs")]
public class GlobalPrefs : Preferences
{
    public GameCameraData initialCameraData;
    public SpaceShipData initialSpaceShipData;
    public SpaceShipData initialSpaceShip2Data;
}