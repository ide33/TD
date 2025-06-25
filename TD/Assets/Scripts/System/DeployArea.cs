using UnityEngine;

public class DeployArea : MonoBehaviour, IDeployArea
{
    public bool IsOccupied { get; set; }
    public bool CanDeploy(DeployableUnitData data)
    {
        // 近接なら配置可能
        return data.allyData .attackType == AllyData.AttackType.Melee;
    }
}
