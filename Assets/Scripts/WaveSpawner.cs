using UnityEngine;
using TMPro;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform spawnPointTransform;
    public float betweenWavesTime = 5f;
    private float countdown = 2f;
    public TextMeshProUGUI countdownText;
    private int waveIndex = 0;
    private Wave wave;
    public GameManager gameManager;
    public static int AliveEnemies { set; get; }

    private void Start()
    {
        AliveEnemies = 0;
    }

    private void Update()
    {
        if (GameManager.isGameOver)
        {
            this.enabled = false;
            return;
        }
        if (AliveEnemies > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = betweenWavesTime;
            return;
        }
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        UpdateCountDown();
    }

    private void UpdateCountDown()
    {
        countdownText.text = string.Format("{0:00.00}", countdown);
    }

    private IEnumerator SpawnWave()
    {
        PlayerStats.rounds += 1;
        wave = waves[waveIndex];
        AliveEnemies = wave.count;
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveIndex += 1;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPointTransform.position, spawnPointTransform.rotation);
    }
}
