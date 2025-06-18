using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : UnitBase
{
    // キャラデータの取得
    [SerializeField] private EnemyData data;

    // 攻撃位置
    [SerializeField] private Transform attackPoint;

    // 最新の状態
    private IEnemyUnit currentState;

    // Enemy固有ステータス
    public int WGT { get; private set;}
    public float MOV { get; private set;}
    public float attackRange { get; private set;}


    // 攻撃位置
    public Transform AttackPoint
    { get { return attackPoint; } }

    public Vector2 MoveDirection { get; private set; } = Vector2.left;
    public Tilemap tilemap;

    public override void Start()
    {
        // ステータスの初期化
        maxHP = data.maxHP;
        STR = data.STR;
        DEF = data.DEF;
        INT = data.INT;
        RES = data.RES;
        WGT = data.WGT;
        MOV = data.MOV;
        attackRange = data.attackRange;

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

        if (this is Enemy)
        {
            // 死亡時にカウント
            GameManager.Instance.EnemyDefeated();
        }

        // 死亡状態に切り替え
        // ChangeState(new EnemyDeadState());
    }
}
