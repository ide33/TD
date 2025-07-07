using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class AreaTilemap : MonoBehaviour
{
    public MapManager.AreaType areaType;
    public Tilemap tilemap;

    private void Start()
    {
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                MapManager.Instance.areaMap[pos] = areaType;
            }
        }   
    }

    public List<Vector3Int> GetUsedCells()
    {
        List<Vector3Int> cells = new List<Vector3Int>();
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                // 元の1マス分登録
                cells.Add(pos);

                // もし2マスタイルなら、隣も登録
                Vector3Int belowCell = new Vector3Int(pos.x, pos.y - 1, pos.z);
                if (!tilemap.HasTile(belowCell))
                {
                    cells.Add(belowCell);
                }
            }
        }

        return cells;
    }
}
