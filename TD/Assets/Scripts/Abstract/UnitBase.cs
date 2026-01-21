using UnityEngine;
using System.Collections.Generic;

// このクラスは直接使えない
public abstract class UnitBase : MonoBehaviour, IUnit
{
    // 自分自身、継承先だけアクセス可能
    [Header("Stats")]
    [SerializeField] protected UnitStats stats;

    // 継承先で参照可能、現在のHP
    protected int currentHP;

    // // ユニットデータ
    public int MaxHP => stats.maxHP;
    public int CurrentHP => currentHP;
    
    public int STR => stats.STR;
    public int DEF => stats.DEF;
    public int INT => stats.INT;
    public int RES => stats.RES;

    public float MOV => stats.MOV;
    public float AttackRange => stats.attackRange;
    public int BLK => stats.BLK;
    public int WGT => stats.WGT;
    public float SP => stats.SP;

    // public List<Vector3> movePath;
    // public int currentPathIndex;


    // 継承先で上書き(override)可能
    public virtual void Start()
    {
        currentHP = stats.maxHP;
    }

    public virtual void TakeDamage(int damage)
    {
        //HPをダメージ分減らす
        currentHP -= damage;

        Debug.Log($"{gameObject.name}は{damage}ダメージを受けた");

        if (IsDead)
        {
            Die();
        }
    }

    public bool IsDead
    {
        get { return currentHP <= 0; }
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name}は倒された");

        Destroy(gameObject);
    }
}