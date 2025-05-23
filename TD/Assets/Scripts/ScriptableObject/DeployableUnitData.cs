using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "DeployableUnit")]
public class DeployableUnitData : ScriptableObject
{
    // ユニットの名前
    public string unitName;

    // ユニットのアイコン
    public Sprite icon;

    // ユニットのプレハブ
    public GameObject unitPrefab;

    // 配置コスト
    public int cost;
}
