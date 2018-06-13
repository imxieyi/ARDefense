using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public static int EnemiesAlive = 0;

	public Transform spawnBase;
	public Transform spawnPoint;

    public Wave[] waves;

	public Text waveCountdownText;

	public float timeBetweenWaves = 5f;
	float countdown = 2f;
	int waveIndex = 0;
    int subWaveIndex = 0;

    bool spawning = false;

    void Start() {
        EnemiesAlive = 0;
    }

    void Update() {
        if (EnemiesAlive > 0) {
            return;
        }
        if (waveIndex == waves.Length) {
            GameManager.instance.WinGame();
            enabled = false;
        }

        if (countdown <= 0f) {
            spawning = true;
            waveCountdownText.text = "WAVE " + (waveIndex + 1) + "/" + waves.Length;
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
		}

        if (spawning) {
            return;
        }

		countdown -= Time.deltaTime;

		countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

		waveCountdownText.text = string.Format("{0:00.00}", countdown);
	}

	IEnumerator SpawnWave() {
        PlayerStats.Waves++;
        Wave wave = waves[waveIndex];
        foreach (var sw in wave.subWaves) {
            for (int i = 0; i < sw.count; i++) {
                Instantiate(sw.enemy, spawnPoint.position, spawnPoint.rotation, spawnBase);
                EnemiesAlive++;
                yield return new WaitForSeconds(1f / sw.rate);
            }
        }
        waveIndex++;
        if (waveIndex < waves.Length) {
            spawning = false;
        }
    }

}
