using UnityEngine;

public interface IEnemyUnit
{
    void EnterState(Enemy enemy);          // 状態に入ったときの処理
    void UpdateState(Enemy enemy);         // 毎フレーム呼ばれる処理
    void ExitState(Enemy enemy);           // 状態を抜けるときの処理
}
