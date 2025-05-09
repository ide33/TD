using UnityEngine;

public class Enemy : UnitBase
{
    [SerializeField] private float moveSpeed = 1.0f;  // 移動速度

    private IEnemyUnit currentState;

    public float MoveSpeed { get { return moveSpeed; } }  // 読み取り専用プロパティ

    public Vector2 MoveDirection { get; private set; } = Vector2.left;

    public override void Start()
    {
        base.Start();  // HP初期化

        ChangeState(new EnemyMoveState());  // 最初は移動状態
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

    // ユニットが死亡したときに呼ばれる処理
    protected override void Die()
    {
        base.Die();

        // 死亡状態に切り替え
        // ChangeState(new EnemyDeadState());
    }
}
