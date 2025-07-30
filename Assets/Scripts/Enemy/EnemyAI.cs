using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public bool grounded = false;

    public GameObject Player;

    public NavMeshAgent navMeshAgent;
    public Animator animator;
    private IEnemyStates _currentState;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        navMeshAgent.speed = moveSpeed;
        animator = GetComponent<Animator>();
        ChangeState(new DetectionState());
    }
    public void ChangeState(IEnemyStates newState)
    {
        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState?.EnterState(this);
    }

        private void Update()
        {
     
            if (grounded)
            {
                navMeshAgent.enabled = true;
                ChangeState(new DetectionState());
            }
          
       

        _currentState?.UpdateState(this);
    }

}
