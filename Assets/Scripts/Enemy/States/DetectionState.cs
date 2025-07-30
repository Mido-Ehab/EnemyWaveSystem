using UnityEngine;

public class DetectionState : IEnemyStates
{
    private Transform player;
    private float detectionRange = 30;

    public void EnterState(EnemyAI enemy)
    {
        player = enemy.Player.transform;
        Debug.Log("Entered Detection State");
    }

    public void UpdateState(EnemyAI enemy)
    {
        if (player == null) return;

        float distance = Vector3.Distance(enemy.transform.position, player.position);

        if (distance <= detectionRange)
        {
            enemy.navMeshAgent.SetDestination(player.position);
            enemy.animator.SetBool("isWalking", true);

            if (distance <= 2f) 
            {
                enemy.ChangeState(new AttackState());
            }
        }
        else
        {
            enemy.animator.SetBool("isWalking", false);
            //enemy.navMeshAgent.ResetPath();
        }
    }

    public void ExitState(EnemyAI enemy)
    {
        enemy.animator.SetBool("isWalking", false);
        Debug.Log("Exiting Detection State");
    }
}
