using System;
using UnityEngine;

namespace G
{
    public abstract class Grid
    {
        public const float TILE_SIZE = 1f;
        
        public static Vector2Int GetTile(Vector2 worldPos) => WorldToTile(worldPos, TILE_SIZE);
        public static Vector2 GetCenter(Vector2Int tilePos) => TileToWorldCenter(tilePos, TILE_SIZE);
        public static GridConfig GetConfig() => Load.LoadConfig<GridConfig>(nameof(Grid));

        public static void ForAllTilesInGrid(Action<Vector2Int> action)
        {
            var config = GetConfig();
            for (var x = 0; x < config.gridSize; x++)
            { for (var y = 0; y < config.gridSize; y++) 
                { action?.Invoke(new Vector2Int(x, y)); } }
        }

        private static Vector2Int WorldToTile(Vector2 worldPos, float tileSize)
        {
            var config = GetConfig();
            var origin = config.origin;
            var x = Mathf.FloorToInt((worldPos.x - origin.x) / tileSize);
            var y = Mathf.FloorToInt((worldPos.y - origin.y) / tileSize);
            return new Vector2Int(x, y);
        }

        private static Vector2 TileToWorldCenter(Vector2Int tilePos, float tileSize)
        {
            var config = GetConfig();
            var origin = config.origin;
            return new Vector2
            (
                origin.x + (tilePos.x + 0.5f) * tileSize,
                origin.y + (tilePos.y + 0.5f) * tileSize
            );
        }
    }
}