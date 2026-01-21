using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

public class Ally : UnitBase
{
    [SerializeField, Tooltip("このユニットの配置コスト（AllyDataから取得）")]

    private IAllyUnit currentState;

    public IAttackStrategy attackStrategy;
    public Transform AttackPoint => transform;


    public override void Start()
    {
        // ステータスの初期化
        // stats = allyStats;

        // キャラに応じて攻撃方法を変える
        switch (stats.attackType)
        {
            // 近接
            case UnitStats.AttackType.Melee:
                attackStrategy = new MeleeAttack();
                break;

            // 遠距離
            case UnitStats.AttackType.Ranged:
                attackStrategy = new RangedAttack();
                break;

            // 魔法
            case UnitStats.AttackType.Magic:
                attackStrategy = new MagicAttack();
                break;
        }

        // SkillManagerに登録
        SkillManager.Instance.RegisterAlly(this);

        // HP初期化
        base.Start();

        // 最初は移動状態
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
        return Physics2D.OverlapCircle(
           transform.position,
           AttackRange,
           LayerMask.GetMask("Enemy")
       ) != null;
    }
}
