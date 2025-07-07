using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyRoute", menuName = "Enemy/Route")]
public class EnemyRoute : ScriptableObject
{
    public List<Vector3Int> routeCells;
}
