using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;

    // 味方ユニットごとのSPを管理
    private Dictionary<Ally, float> allySPDict = new Dictionary<Ally, float>();

    [SerializeField] private float gainSP = 10f;
    [SerializeField] private float maxSP = 100f;

    public float MaxSP => maxSP;

    private void Awake()
    {
        // 自分自身をInstanceに登録
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // 登録(Allyのスタートで呼ぶ)
    public void RegisterAlly(Ally ally)
    {
        if (!allySPDict.ContainsKey(ally))
        {
            allySPDict[ally] = 0f;
        }
    }

    // SP加算処理
    public void AddSP(Ally ally)
    {
        if (!allySPDict.ContainsKey(ally)) return;

        // maxSPを超えないように制限
        float newSP = allySPDict[ally] + gainSP;
        newSP = Mathf.Min(newSP, maxSP);

        allySPDict[ally] = newSP;
        ally.SetSP(newSP);

        Debug.Log($"{ally.name} のSP：{allySPDict[ally]}");
    }

    public bool CanUseSkill(Ally ally)
    {
        return allySPDict.ContainsKey(ally) && allySPDict[ally] >= maxSP;
    }

    public void ResetSP(Ally ally)
    {
        if (allySPDict.ContainsKey(ally))
        {
            allySPDict[ally] = 0f;
            ally.SetSP(0f);
        }
    }

    public float GetSP(Ally ally)
    {
        return allySPDict.ContainsKey(ally) ? allySPDict[ally] : 0f;
    }
}
