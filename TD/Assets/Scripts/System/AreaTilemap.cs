using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class AreaTilemap : MonoBehaviour
{
    public MapManager.AreaType areaType;
    public Tilemap tilemap;

    public List<Vector3Int> GetUsedCells()
    {
        List<Vector3Int> cells = new List<Vector3Int>();
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                cells.Add(pos);
            }
        }

        return cells;
    }
}
