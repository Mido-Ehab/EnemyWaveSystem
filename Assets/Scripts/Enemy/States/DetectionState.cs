using UnityEngine;

public class DetectionState : IEnemyStates
{
    private Transform player;

    // How far the enemy can detect the player
    private float detectionRange = 30;

    public void EnterState(EnemyAI enemy)
    {
        player = enemy.Player.transform;
    }

    public void UpdateState(EnemyAI enemy)
    {
        if (player == null) return;

        // Calculate the distance to the player
        float distance = Vector3.Distance(enemy.transform.position, player.position);

        // If the player is within detection range, move towards them
        if (distance <= detectionRange)
        {
            enemy.navMeshAgent.SetDestination(player.position);
            enemy.animator.SetBool("isWalking", true);

            //transition to attack state
            if (distance <= 4f) 
            {
                enemy.ChangeState(new AttackState());
            }
        }
        else
        {
            // Stop walking animation if player is out of range
            enemy.animator.SetBool("isWalking", false);
        }
    }

    public void ExitState(EnemyAI enemy)
    {
        enemy.animator.SetBool("isWalking", false);
    }
}
