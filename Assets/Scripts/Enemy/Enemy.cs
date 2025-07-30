using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    private void OnMouseDown()
    {
        Kill();
    }

    public void Kill()
    {
        gameObject.SetActive(false);
        WaveManager.Instance.RemoveEnemy(gameObject);
        WaveManager.Instance.OnEnemyKilled();
    }
}
