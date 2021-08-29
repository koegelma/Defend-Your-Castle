using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

    public static int EnemiesAlive = 0;
    public Wave[] waves;
    public Transform spawnPoint;
    public float timeBetweenWaves = 10.5f;
    private float countdown = 5.5f;
    public Text waveCountdownText;

    private int waveIndex = 0;

    private void Update()
    {
        if (EnemiesAlive > 0) return;

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        waveCountdownText.text = (/* "NEXT WAVE: " +  */Mathf.Round(countdown).ToString());
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        if (!wave.hasEnemyVariation)
        {
            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }

        if (wave.hasEnemyVariation)
        {
            for (int i = 0; i < wave.count; i++)
            {
                if (i % wave.variationModulo == 0) SpawnEnemy(wave.enemyVariation);
                else SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }

        waveIndex++;

        if (waveIndex == waves.Length)
        {
            Debug.Log("LEVEL WON!");
            this.enabled = false;
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
}
