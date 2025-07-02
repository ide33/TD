using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PathFinder : MonoBehaviour
{
    // PathFinderのシングルトン
    public static PathFinder Instance { get; private set; }

    // マップ情報を格納する辞書
    public Dictionary<Vector3Int, MapManager.AreaType> areaMap;

    // 経路を保存するリスト
    public List<Vector3Int> currentPath;

    // 確認用のデフォルト座標
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

    private void Start()
    {
        // MapManagerからareaMapを取得
        areaMap = MapManager.Instance.areaMap;

        if (areaMap == null)
        {
            Debug.LogError("areaMapがnullです");
        }
        else
        {
            Debug.Log("PathFinder: areaMapが正常に読み込まれました。");
        }
    }

    public List<Vector3Int> FindPath(Vector3Int start, Vector3Int goal)
    {
        Debug.Log($"spawnCell:{startCell}, isDeployArea: {areaMap[startCell] == MapManager.AreaType.DeployArea}");
        Debug.Log($"goalCell:{goalCell}, isDeployArea: {areaMap[goalCell] == MapManager.AreaType.DeployArea}");

        // 幅優先探索(BFS)用のキューと経路復元用の履歴辞書
        Queue<Vector3Int> queue = new Queue<Vector3Int>();
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();

        // 探索スタート地点を登録
        queue.Enqueue(start);
        cameFrom[start] = start;

        // 上下左右の移動方向
        Vector3Int[] directions = { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right };

        // BFSで探索、ゴールについたら終了
        while (queue.Count > 0)
        {
            Vector3Int current = queue.Dequeue();

            if (current == goal)
                break;

            // 隣接するマスを調査
            foreach (Vector3Int dir in directions)
            {
                // 通れない、既に調べた、マップ外などをスキップ
                Vector3Int next = current + dir;
                if (!areaMap.ContainsKey(next)) continue;
                if (areaMap[next] != MapManager.AreaType.DeployArea) continue;
                if (cameFrom.ContainsKey(next)) continue;

                // 次のマスを探索予定に加える
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
