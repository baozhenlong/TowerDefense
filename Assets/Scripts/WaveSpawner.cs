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

    private void Update()
    {

        if (waveIndex == waves.Length)
        {
            Debug.Log("波次生成结束");
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
        wave = waves[waveIndex];
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
