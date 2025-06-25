using UnityEngine;

public interface IDeployArea
{
    bool IsOccupied { get; set; }
    bool CanDeploy(DeployableUnitData data);
}
