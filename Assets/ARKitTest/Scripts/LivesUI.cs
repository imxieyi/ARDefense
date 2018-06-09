using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour {

    public Text livesText;
	
	// Update is called once per frame
	void Update () {
        livesText.text = string.Format("{0:00}", PlayerStats.Lives < 0 ? 0 : PlayerStats.Lives);
	}
}
