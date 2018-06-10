using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWin : MonoBehaviour {

    public Canvas canvas;
	
	// Update is called once per frame
    void Update () {
        var dir = Camera.main.transform.forward;
        dir.y = 0;
        canvas.transform.rotation = Quaternion.LookRotation(dir);
	}

}
