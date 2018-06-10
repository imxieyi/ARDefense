using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWin : MonoBehaviour {

    public Canvas canvas;
    public Text exitTip;
    public Canvas overlay;
    public Canvas mainUI;

    void Start() {
        Invoke("AllowExit", 10);
        mainUI.gameObject.SetActive(false);
    }

    void AllowExit() {
        exitTip.gameObject.SetActive(true);
        overlay.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
        var dir = Camera.main.transform.forward;
        dir.y = 0;
        canvas.transform.rotation = Quaternion.LookRotation(dir);
	}

}
