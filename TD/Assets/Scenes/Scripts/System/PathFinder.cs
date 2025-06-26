using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PathFinder : MonoBehaviour
{
    public static PathFinder Instance { get; private set; }

    public Dictionary<Vector3Int, MapManager.AreaType> areaMap;
    public List<Vector3Int> currentPath;

    [SerializeField] private Vector3Int startCell;
    [SerializeField] private Vector3Int goalCell;

    private void Awake()
    {
        // 自分自身をInstanceに登録
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public List<Vector3Int> FindPath(Vector3Int start, Vector3Int goal)
    {
        Queue<Vector3Int> queue = new Queue<Vector3Int>();
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();

        queue.Enqueue(start);
        cameFrom[start] = start;

        Vector3Int[] directions = { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right };

        while (queue.Count > 0)
        {
            Vector3Int current = queue.Dequeue();

            if (current == goal)
                break;

            foreach (Vector3Int dir in directions)
            {
                Vector3Int next = current + dir;
                if (!areaMap.ContainsKey(next)) continue;
                if (areaMap[next] != MapManager.AreaType.DeployArea) continue;
                if (cameFrom.ContainsKey(next)) continue;

                cameFrom[next] = current;
                queue.Enqueue(next);
            }
        }

        // 経路が見つからなかった場合
        if (!cameFrom.ContainsKey(goal))
        {
            Debug.LogWarning("経路が見つかりませんでした");
            return new List<Vector3Int>();
        }

        // 経路を逆順にたどって作成
        List<Vector3Int> path = new List<Vector3Int>();
        Vector3Int temp = goal;
        while (temp != start)
        {
            path.Add(temp);
            temp = cameFrom[temp];
        }
        path.Add(start);
        path.Reverse();

        currentPath = path;
        return path;
    }
}
