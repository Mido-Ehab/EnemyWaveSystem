using UnityEngine;

public class Enemy : MonoBehaviour
{

    // click on the enemy to kill it
    private void OnMouseDown()
    {
        Kill();
    }

    // Disables the enemy and notifies the WaveManager about the kill
    public void Kill()
    {
        gameObject.SetActive(false);
        WaveManager.Instance.RemoveEnemy(gameObject);
        WaveManager.Instance.OnEnemyKilled();
    }
}
