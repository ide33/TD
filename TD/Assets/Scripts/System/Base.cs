using UnityEngine;

public class Base : MonoBehaviour
{
    public void TakeDamage(int amount)
    {
        GameManager.Instance.DamageBase(amount);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}
