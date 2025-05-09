using UnityEngine;

// このクラスは直接使えない
public abstract class UnitBase : MonoBehaviour, IUnit
{
    [SerializeField] protected int maxHP = 100;  // 自分自身、継承先だけアクセス可能

    protected int currentHP;  // 継承先で参照可能、現在のHP

    public virtual void Start()  // 継承先で上書き(override)可能
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHP -= damage;  //HPをダメージ分減らす

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
