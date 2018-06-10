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

    bool spawning = false;

	void Update() {
        if (EnemiesAlive > 0) {
            return;
        }

		if (countdown <= 0f) {
            spawning = true;
            waveCountdownText.text = "WAVE " + (waveIndex + 1);
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
        for (int i = 0; i < wave.count; i++) {
            Instantiate(wave.enemy, spawnPoint.position, spawnPoint.rotation, spawnBase);
            EnemiesAlive++;
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveIndex++;
        if (waveIndex == waves.Length) {
            enabled = false;
        }
        spawning = false;
    }

}
