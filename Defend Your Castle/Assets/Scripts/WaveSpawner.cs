using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 8.5f;
    private float countdown = 3.5f;
    public Text waveCountdownText;
    private int waveIndex = 0;
    private int enemyCount = 0;
    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;

        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        enemyCount = waveIndex;

        Debug.Log("Wave: " + waveIndex);

        if (enemyCount >= 3)
        {
            enemyCount = waveIndex + (int)Random.Range(enemyCount * -0.4f, enemyCount * 0.4f);
        }

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}
