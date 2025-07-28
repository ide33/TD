using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

public class Ally : UnitBase
{
    // キャラデータの取得
    [SerializeField] private AllyData data;

    [SerializeField, Tooltip("このユニットの配置コスト（AllyDataから取得）")]

    // 最新の状態
    private IAllyUnit currentState;

    // private SPUI spUI;

    // 攻撃方法
    public IAttackStrategy attackStrategy;

    // Ally固有ステータス
    public int BLK { get; private set; }
    public float SP { get; private set; }
    public float attackRange { get; private set; }

    public void SetSP(float value)
    {
        SP = Mathf.Min(value, SkillManager.Instance.MaxSP);

        // UI反映
        // spUI?.SetSP(SP);
    }

    // public void SetSPUI(SPUI ui)
    // {
    //     spUI = ui;

    //     // 初期値反映
    //     // ui.SetSP(SP);
    // }

    // 攻撃位置
    public Transform AttackPoint
    { get { return transform; } }

    public override void Start()
    {
        // ステータスの初期化
        maxHP = data.maxHP;
        STR = data.STR;
        DEF = data.DEF;
        INT = data.INT;
        RES = data.RES;
        BLK = data.BLK;
        SP = data.SP;
        attackRange = data.attackRange;

        // キャラに応じて攻撃方法を変える
        switch (data.attackType)
        {
            // 近接
            case AllyData.AttackType.Melee:
                attackStrategy = new MeleeAttack();
                break;

            // 遠距離
            case AllyData.AttackType.Ranged:
                attackStrategy = new RangedAttack();
                break;

            // 魔法
            case AllyData.AttackType.Magic:
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
        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Enemy"));

        return hit != null;
    }
}
