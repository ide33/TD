using UnityEngine;

[CreateAssetMenu(fileName = "UnitStats", menuName = "Unit/UnitStats", order = 1)]
public class UnitStats : ScriptableObject
{
    [Header("Basic")]
    public int maxHP;

    [Header("Status")]
    public int STR;
    public int DEF;
    public int INT;
    public int RES;

    [Header("Attack")]
    public AttackType attackType;
    public float attackRange;

    [Header("Movement")]
    public float MOV;
    public int BLK;

    [Header("Other")]
    public int WGT;
    public float SP;

    public enum AttackType
    {
        Melee,
        Ranged,
        Magic
    }
}