using UnityEngine;
using System.Collections.Generic;

// このクラスは直接使えない
public abstract class UnitBase : MonoBehaviour, IUnit
{
    // 自分自身、継承先だけアクセス可能
    [SerializeField] protected int maxHP = 100;

    // 継承先で参照可能、現在のHP
    protected int currentHP;

    // ユニットデータ
    public int STR;
    public int DEF;
    public int INT;
    public int RES;

    public List<Vector3> movePath;
    public int currentPathIndex;


    // 継承先で上書き(override)可能
    public virtual void Start()
    {
        currentHP = maxHP;
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
