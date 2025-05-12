using UnityEngine;

public class Enemy : UnitBase
{
    // 移動速度
    [SerializeField] private float moveSpeed = 1.0f;

    // 攻撃範囲
    [SerializeField] private float attackRange = 0.5f;

    // 攻撃位置
    [SerializeField] private Transform attackPoint;

    // 最新の状態
    private IEnemyUnit currentState;

    // 読み取り専用プロパティ
    public float MoveSpeed { get { return moveSpeed; } }

    // attackRange取得用プロパティ
    public float AttackRange
    { get { return attackRange; } }

    // 攻撃位置
    public Transform AttackPoint
    { get { return attackPoint; } }

    public Vector2 MoveDirection { get; private set; } = Vector2.left;

    public override void Start()
    {
        // HP初期化
        base.Start();

        // 最初は移動状態
        ChangeState(new EnemyMoveState());
    }

    public void ChangeState(IEnemyUnit newState)
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

    private void Update()
    {
        // 現在の状態がnullでなければUpdateを呼ぶ
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public bool IsAllyInRange()
    {
        // Enemyの敵が範囲内にいるか調べる
        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Ally"));

        return hit != null;
    }

    // ユニットが死亡したときに呼ばれる処理
    protected override void Die()
    {
        base.Die();

        // 死亡状態に切り替え
        // ChangeState(new EnemyDeadState());
    }
}
