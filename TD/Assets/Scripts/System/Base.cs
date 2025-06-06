using UnityEngine;

public class Base : MonoBehaviour
{
    public void TakeDamage(int amount)
    {
        // GameManagerのDamageBaseを呼び出し
        GameManager.Instance.DamageBase(amount);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // 自陣にダメージを与えて敵を破壊
            TakeDamage(1);
            Destroy(other.gameObject);

            Debug.Log("自陣の耐久値が1減少");
        }
    }
}
