using UnityEngine;

public class HighGroundArea : MonoBehaviour, IDeployArea
{
    public bool IsOccupied { get; set; }
    [SerializeField] private UnitStats allyStats;
    
    public bool CanDeploy(DeployableUnitData data)
    {
        // 遠距離、魔法なら配置可能
        return allyStats.attackType == UnitStats.AttackType.Ranged || allyStats.attackType == UnitStats.AttackType.Magic;
    }
}
