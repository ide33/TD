// using UnityEngine;
// using System.Collections.Generic;

// [RequireComponent(typeof(LineRenderer))]
// public class PathVisualizer : MonoBehaviour
// {
//     // 線描画に使うコンポーネント
//     private LineRenderer line;

//     private void Start()
//     {
//         // PathFinderからパスを取得、描画
//         line = GetComponent<LineRenderer>();
        
//         if (PathFinder.Instance != null && PathFinder.Instance.currentPath != null && PathFinder.Instance.currentPath.Count > 0)
//         {
//             DrawPath(PathFinder.Instance.currentPath);
//         }
//         else
//         {
//             Debug.Log("PathVisualizer: 経路が設定されていないため、描画をスキップします");
//         }
//     }

//     public void DrawPath(List<Vector3Int> path)
//     {
//         // 描画する点の数を設定
//         if (path == null || path.Count == 0) return;

//         line.positionCount = path.Count;

//         // 各点をワールド座標に変換して描画
//         for (int i = 0; i < path.Count; i++)
//         {
//             Vector3 worldPos = MapManager.Instance.mainTilemap.GetCellCenterWorld(path[i]);
//             line.SetPosition(i, worldPos + Vector3.back * 0.1f);
//         }
//     }
// }
