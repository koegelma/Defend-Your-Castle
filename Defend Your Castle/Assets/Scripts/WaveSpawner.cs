using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 10.5f;
    private float countdown = 5.5f;
    public Text waveCountdownText;
    private int waveIndex = 0;
    private int enemyCount = 0;


    private bool isWave = false;


    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        if (!isWave)
        {
            countdown -= Time.deltaTime;
            waveCountdownText.text = ("Next Wave: " + Mathf.Round(countdown));
        }

        if (isWave)
        {
            waveCountdownText.text = ("");
        }

    }

    IEnumerator SpawnWave()
    {
        isWave = true;
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
        isWave = false;
    }
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}
