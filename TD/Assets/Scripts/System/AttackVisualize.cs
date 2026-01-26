using UnityEngine;

public class AttackVisualize : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    // private Transform target;
     private UnitBase target;
    private System.Action onHit;

    public void Initialize(UnitBase target, System.Action onHit)
    {
        Debug.Log("Initialize called");
        this.target = target;
        this.onHit = onHit;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            onHit?.Invoke();
            Destroy(gameObject);
        }
    }
}
