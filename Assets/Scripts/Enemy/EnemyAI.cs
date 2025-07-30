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
    // Called on the first frame - sets up player, speed, and state
    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        navMeshAgent.speed = moveSpeed;
        animator = GetComponent<Animator>();
        ChangeState(new DetectionState());
    }
    // Changes the current enemy state to a new one
    public void ChangeState(IEnemyStates newState)
    {
        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState?.EnterState(this);
    }

    // Called every frame - updates the current state behavior
    private void Update()
    {  
             _currentState?.UpdateState(this);
    }

}
