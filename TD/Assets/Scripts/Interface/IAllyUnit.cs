using System.Security;
using UnityEngine;

public interface IAllyUnit
{
    void EnterState(Ally ally);   // 状態に入ったときの処理
    void UpdateState(Ally ally);  // 毎フレーム呼ばれる処理
    void ExitState(Ally ally);    // 状態を抜けるときの処理
}
