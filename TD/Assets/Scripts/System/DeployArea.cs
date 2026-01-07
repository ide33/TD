using UnityEngine;

public class DeployArea : MonoBehaviour, IDeployArea
{
    public bool IsOccupied { get; set; }
    [SerializeField] private UnitStats allyStats;
    
    public bool CanDeploy(DeployableUnitData data)
    {
        // 近接なら配置可能
        return allyStats.attackType == UnitStats.AttackType.Melee;
    }
}
