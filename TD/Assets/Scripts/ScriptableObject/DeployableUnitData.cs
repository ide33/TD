using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "DeployableUnit")]
public class DeployableUnitData : ScriptableObject
{
    // ユニットの名前
    public string unitName;

    // ユニットのアイコン
    public Sprite icon;

    // 実体ユニットのプレハブ
    public GameObject unitprefab;

    // ゴーストユニットのプレハブ
    public GameObject ghostprefab;

    // // 配置コスト
    public int cost;

    // AllyDataのコストにアクセス
    public AllyData allyData;
}
