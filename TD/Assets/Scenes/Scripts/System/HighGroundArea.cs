using UnityEngine;

public class HighGroundArea : MonoBehaviour, IDeployArea
{
    public bool IsOccupied { get; set; }
    public bool CanDeploy(DeployableUnitData data)
    {
        // 遠距離、魔法なら配置可能
        return data.allyData.attackType == AllyData.AttackType.Ranged || data.allyData.attackType == AllyData.AttackType.Magic;
    }
}
