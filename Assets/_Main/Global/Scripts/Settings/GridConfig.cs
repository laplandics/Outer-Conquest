using UnityEngine;

[CreateAssetMenu(fileName ="Grid",menuName ="Config/Grid")]
public class GridConfig : Config
{
    public Vector2Int origin;
    [Range(10, 1000)] public int gridSize;
}