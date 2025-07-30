using UnityEngine;

public interface IEnemyStates
{
    public void EnterState(EnemyAI enemy);
    public void UpdateState(EnemyAI enemy);
    public void ExitState(EnemyAI enemy);
}
