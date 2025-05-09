using UnityEngine;

public interface IUnit
{
    void TakeDamage(int amout);  // ダメージを受ける
    bool IsDead{get;}            // 死亡判定
}
