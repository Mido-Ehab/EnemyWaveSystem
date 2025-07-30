using UnityEngine;

public class AttackState : IEnemyStates
{
    private Transform player;
    private float attackCooldown = 1.5f;
    private float lastAttackTime;

    public void EnterState(EnemyAI enemy)
    {
        player =enemy.Player.transform;
        enemy.navMeshAgent.ResetPath();
        enemy.animator.SetTrigger("attack");
        Debug.Log("Entered Attack State");
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

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            enemy.animator.SetTrigger("attack");
           
        }

        enemy.transform.LookAt(new Vector3(player.position.x, enemy.transform.position.y, player.position.z));
    }

    public void ExitState(EnemyAI enemy)
    {
        Debug.Log("Exiting Attack State");
    }
}
