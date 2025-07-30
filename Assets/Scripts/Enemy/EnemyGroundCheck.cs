using Unity.VisualScripting;
using UnityEngine;

public class EnemyGroundCheck : MonoBehaviour
{
    private EnemyAI enemyAI;

    private void Start()
    {
        enemyAI = GetComponentInParent<EnemyAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger || !other.CompareTag("Player")) 
        {
            enemyAI.grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger || !other.CompareTag("Player"))
        {
            enemyAI.grounded = false;
        }
    }
}
