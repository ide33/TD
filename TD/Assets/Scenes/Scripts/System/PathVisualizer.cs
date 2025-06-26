using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class PathVisualizer : MonoBehaviour
{
    private LineRenderer line;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        DrawPath(PathFinder.Instance.currentPath);
    }

    public void DrawPath(List<Vector3Int> path)
    {
        if (path == null || path.Count == 0) return;

        line.positionCount = path.Count;
        for (int i = 0; i < path.Count; i++)
        {
            Vector3 worldPos = MapManager.Instance.tilemap.GetCellCenterWorld(path[i]);
            line.SetPosition(i, worldPos + Vector3.back * 0.1f);
        }
    }
}
