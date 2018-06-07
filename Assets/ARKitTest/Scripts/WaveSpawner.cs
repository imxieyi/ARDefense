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
	private float countdown = 2f;
	private int waveIndex = 0;

	void Update() {
		if (countdown <= 0f) {
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
		}
		countdown -= Time.deltaTime;
		waveCountdownText.text = Mathf.Round(countdown).ToString();
	}

	IEnumerator SpawnWave() {
		waveIndex++;
		for (int i = 0; i < waveIndex; i++) {
			Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation, spawnBase);
			yield return new WaitForSeconds(0.5f);
		}
	}

}
