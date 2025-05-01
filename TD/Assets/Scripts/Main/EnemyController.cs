using UnityEngine;
using UnityEngine.XR;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 2.0f;
    private IEnemyUnit currentState;

    private void Start()
    {
        ChangeState(new Move(this));
    }

    private void Update()
    {
        currentState?.UpdateState();
    }

    public void ChangeState(IEnemyUnit newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}
