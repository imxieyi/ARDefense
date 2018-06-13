using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
	public static float Money;
    	public static int Lives;
    	public int startLives = 20;

    	public static int Waves;
	
	[Header("Money")]
	public float startMoney = 400;
	public float moneyIncreaseRate = 10;
	void Start() {
		Money = startMoney;
		Lives = startLives;
		Waves = 0;
	}
	
	// update player status per frame
    	void Update()
    	{
		// update money over time
		PlayerStats.Money += moneyIncreaseRate * Time.deltaTime;
    	}
}
