using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    bool gameEnded = false;

    public GameObject gameOverUI;
    public GameObject gameWin;

    public static GameManager instance;

    void Start() {
        instance = this;
    }

    // Update is called once per frame
    void Update () {
        if (gameEnded) {
            return;
        }
        if (PlayerStats.Lives <= 0) {
            EndGame();
        }
	}

    public void WinGame() {
        gameWin.SetActive(true);
    }

    void EndGame() {
        Time.timeScale = 0f;
        gameEnded = true;
        gameOverUI.SetActive(true);
    }

}
