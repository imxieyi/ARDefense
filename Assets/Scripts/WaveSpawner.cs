using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public Transform spawnBase;
	public Transform enemyPrefab;
	public Transform spawnPoint;

	public Text waveCountdownText;

	public float timeBetweenWaves = 5f;
	float countdown = 2f;
	int waveIndex = 0;

    bool spawning = false;

	void Update() {
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
		waveIndex++;
        PlayerStats.Waves++;
		for (int i = 0; i < waveIndex; i++) {
			Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation, spawnBase);
			yield return new WaitForSeconds(0.5f);
		}
        spawning = false;
	}

}
