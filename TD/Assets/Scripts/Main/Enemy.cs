using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : UnitBase
{
    // 攻撃位置
    [SerializeField] private Transform attackPoint;

    // ルートを取得
    [Header("Route")]
    [SerializeField] private EnemyRouteObject route;

    public EnemyRouteObject Route => route;

    // 最新の状態
    private IEnemyUnit currentState;
    public Transform AttackPoint => attackPoint;

    // 実行時ステータス
    public float CurrentMoveSpeed { get; private set; }

    public Vector2 MoveDirection { get; private set; } = Vector2.left;

    // public Tilemap tilemap;
    public Vector3Int spawnCell;
    public Vector3Int goalCell;

    public override void Start()
    {
        // ステータスの初期化
        // stats = enemyStats;
        CurrentMoveSpeed = stats.MOV;

        // HP初期化
        base.Start();

        // 最初は移動状態
        ChangeState(new EnemyMoveState());
    }

    private void Update()
    {
        // 現在の状態がnullでなければUpdateを呼ぶ
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
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

    public void SetMoveSpeed(float value)
    {
        CurrentMoveSpeed = Mathf.Max(0, value);
    }

    public void ResetMoveSpeed()
    {
        CurrentMoveSpeed = stats.MOV;
    }

    public bool IsAllyInRange()
    {
        // Enemyの敵が範囲内にいるか調べる
        return Physics2D.OverlapCircle(
            transform.position,
            AttackRange,
            LayerMask.GetMask("Ally")
        ) != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HighGroundArea"))
        {
            Debug.Log($"{name}がHighGroundAreaに到着しました");
            SetMoveSpeed(0);
        }
    }

    // ユニットが死亡したときに呼ばれる処理
    protected override void Die()
    {
        base.Die();

        if (this is Enemy)
        {
            // 死亡時にカウント
            GameManager.Instance.EnemyDefeated();
        }

        // 死亡状態に切り替え
        // ChangeState(new EnemyDeadState());
    }
}
