using UnityEngine;

public interface IEnemyUnit
{
    void EnterState();          // 状態に入ったときの処理
    void UpdateState();         // 毎フレーム呼ばれる処理
    void ExitState();           // 状態を抜けるときの処理
}
