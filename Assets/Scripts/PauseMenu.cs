using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject ui;
    public Animator animator;

    public void PauseGame() {
        ui.SetActive(true);
        Debug.Log("Game Paused");
        animator.SetBool("ShowMenu", true);
        Time.timeScale = 0f;
    }

    public void ResumeGame() {
        animator.SetBool("ShowMenu", false);
        Time.timeScale = 1f;
        Invoke("ResumeGameInternal", 5f / 6f);
    }

    void ResumeGameInternal() {
        Debug.Log("Game Resumed");
        ui.SetActive(false);
    }

    public void Retry() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}
