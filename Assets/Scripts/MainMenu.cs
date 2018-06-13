using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    void Start() {
        Time.timeScale = 1f;
    }

    public void EnterGame() {
        SceneManager.LoadScene("WithTerrain");
    }

}
