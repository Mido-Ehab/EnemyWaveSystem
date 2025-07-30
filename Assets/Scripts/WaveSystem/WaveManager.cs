using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    [Header("Enemy Prefabs (3 Types)")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Scene Enemies")]
    [SerializeField] private Transform initialEnemiesParent;
    [SerializeField] private SpawnArea spawnArea;

    [Header("Wave Settings")]
    [SerializeField] private float delayBetweenWaves = 5f;

    private List<GameObject> initialEnemies = new List<GameObject>();
    private List<GameObject> extraEnemies = new List<GameObject>();
    private List<GameObject> activeEnemies = new List<GameObject>();

    private int currentWave = 1;
    private int totalEnemiesToSpawn;
    private int enemiesKilled = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Cache all manually placed enemies
        foreach (Transform child in initialEnemiesParent)
        {
            child.gameObject.SetActive(false);
            initialEnemies.Add(child.gameObject);
        }

        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        if (IsWaveRunning)
        {
            enemiesKilled = 0;
            totalEnemiesToSpawn = CalculateEnemyCount(currentWave);

            Debug.Log($"Starting Wave {currentWave} with {totalEnemiesToSpawn} enemies.");


            foreach (var enemy in initialEnemies)
                enemy.SetActive(false);

            activeEnemies.Clear();

            // Enable from the manually placed Enemies 
            int available = Mathf.Min(totalEnemiesToSpawn, initialEnemies.Count);
            for (int i = 0; i < available; i++)
            {
                var enemy = initialEnemies[i];
                enemy.transform.position = spawnArea.GetRandomPointInside();
                enemy.SetActive(true);
                activeEnemies.Add(enemy);
            }

            //  wave 3+
            int extraNeeded = totalEnemiesToSpawn - available;
            for (int i = 0; i < extraNeeded; i++)
            {
                GameObject enemy;
                if (i < extraEnemies.Count)
                {
                    enemy = extraEnemies[i];
                }
                else
                {

                    GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                    enemy = Instantiate(prefab);
                    enemy.AddComponent<Enemy>();
                    extraEnemies.Add(enemy);
                }

                enemy.transform.position = spawnArea.GetRandomPointInside();
                enemy.SetActive(true);
                activeEnemies.Add(enemy);
            }

        }
        yield return null;
    }

    private int CalculateEnemyCount(int wave)
    {
        if (wave == 1) return 30;
        if (wave == 2) return 50;
        if (wave == 3) return 70;
        return 70 + (wave - 3) * 10;
    }

    public void OnEnemyKilled()
    {
        enemiesKilled++;
        if (enemiesKilled >= totalEnemiesToSpawn)
        {
            Debug.Log($"Wave {currentWave} cleared.");
            StartCoroutine(NextWaveAfterDelay());
        }
    }

    private IEnumerator NextWaveAfterDelay()
    {
        yield return new WaitForSeconds(delayBetweenWaves);
        if (!IsWaveRunning)
            yield break;
        currentWave++;
        StartCoroutine(StartWave());
    }


    #region UI Ref

    public int GetCurrentWave() => currentWave;
    public int GetActiveEnemyCount() => activeEnemies.Count;
    public void RemoveEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }
    public bool IsWaveRunning { get; private set; } = true;

    public void StopWave()
    {
        IsWaveRunning = false;
    }

    public void ResumeWave()
    {
        IsWaveRunning = true;

        if (activeEnemies.Count <= 0)
        {
            StartCoroutine(NextWaveAfterDelay());
        }
        else
            return;
    }

    public void ForceNextWave()
    {
        StopAllCoroutines();
        currentWave++;
        StartCoroutine(StartWave());
    }

    public void KillAllEnemies()
    {
        foreach (var enemy in activeEnemies)
        {
            if (enemy != null)
                enemy.SetActive(false);
        }
        activeEnemies.Clear();
        StartCoroutine(NextWaveAfterDelay());
    }

    #endregion
}
