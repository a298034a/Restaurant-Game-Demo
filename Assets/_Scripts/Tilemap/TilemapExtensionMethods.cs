using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Restaurant
{
    public static class TilemapExtentionMethods
    {
        public static Vector3Int[] GetAllTiles(this Tilemap tilemap) 
        {
            TileBase white = Resources.Load<TileBase>("Tiles/white");

            BoundsInt bounds = tilemap.cellBounds;
            int maxX = bounds.max.x;
            int maxY = bounds.max.y;

            List<Vector3Int> tileBases = new List<Vector3Int>();

            for (int x = bounds.min.x; x < maxX; x++) 
            {
                for (int y = bounds.min.y; y < maxY; y++)
                {
                    Vector3Int vector3Int = new Vector3Int(x, y, 0);
                    if (tilemap.GetTile(vector3Int) == white) 
                    {
                        tileBases.Add(new Vector3Int(x, y, 0));
                    }                    
                }
            }

            return tileBases.ToArray();
        }
    }
}
