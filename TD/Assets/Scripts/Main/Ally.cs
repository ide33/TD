using UnityEngine;
using UnityEngine.XR;

public class Ally : MonoBehaviour
{
    // 攻撃範囲
    [SerializeField] private float attackRange = 1.0f;

    // 最新の状態
    private IAllyUnit currentState;

    // attackRange取得用プロパティ
    public float AttackRange
    { get { return attackRange; } }

    // 攻撃位置
    public Transform AttackPoint
    { get { return transform; } }

    private void Start()
    {
        //最初は待機状態
        ChangeState(new AllyIdleState());
    }

    private void Update()
    {
        // 現在の状態がnullでなければUpdateを呼ぶ
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public void ChangeState(IAllyUnit newState)
    {
        // 現在の状態がnullでなければExitを呼ぶ
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        // 状態を新しいものに切り替える
        currentState = newState;

        // 新しい状態のEnterを呼ぶ
        if (currentState != null)
        {
            currentState.EnterState(this);
        }
    }

    public bool IsEnemyInRange()
    {
        // Enemyの敵が範囲内にいるか調べる
        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Enemy"));

        return hit != null;
    }
}
