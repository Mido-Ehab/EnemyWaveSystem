using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveUIManager : MonoBehaviour
{
    [Header("References")]
    public WaveManager waveManager;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI enemyCountText;
    public TextMeshProUGUI fpsText;

    public Button stopResumeButton;
    public Button nextWaveButton;
    public Button killAllButton;

    private bool isPaused = false;

    void Start()
    {
        stopResumeButton.onClick.AddListener(TogglePauseWave);
        nextWaveButton.onClick.AddListener(() => waveManager.ForceNextWave());
        killAllButton.onClick.AddListener(() => waveManager.KillAllEnemies());
    }

    void Update()
    {
        waveText.text = "Wave: " + waveManager.GetCurrentWave();
        enemyCountText.text = "Enemies: " + waveManager.GetActiveEnemyCount();
        fpsText.text = $"FPS: {(1f / Time.unscaledDeltaTime):F1}";
    }

    void TogglePauseWave()
    {
        if (isPaused)
        {
            waveManager.ResumeWave();
            stopResumeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Stop Wave";
        }
        else
        {
            waveManager.StopWave();
            stopResumeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Resume Wave";
        }

        isPaused = !isPaused;
    }
}
