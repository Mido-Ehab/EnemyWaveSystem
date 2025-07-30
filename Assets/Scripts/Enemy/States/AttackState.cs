using UnityEngine;

public class AttackState : IEnemyStates
{
    private Transform player;
    private float attackCooldown = 1.5f;// Time between attacks
    private float lastAttackTime;// Timestamp of last attack

    public void EnterState(EnemyAI enemy)
    {
        player =enemy.Player.transform;
        enemy.navMeshAgent.ResetPath();
        enemy.animator.SetTrigger("attack");
    }

    public void UpdateState(EnemyAI enemy)
    {
        if (player == null) return;

        float distance = Vector3.Distance(enemy.transform.position, player.position);

        if (distance > 4f)
        {
            enemy.ChangeState(new DetectionState());
            return;
        }

        // Attack only if cooldown has passed
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            enemy.animator.SetTrigger("attack");
           
        }

        // Keep Rotating to face the player
        enemy.transform.LookAt(new Vector3(player.position.x, enemy.transform.position.y, player.position.z));
    }

    public void ExitState(EnemyAI enemy)
    {
      
    }
}
