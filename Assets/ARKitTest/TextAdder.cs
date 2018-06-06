using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAdder : MonoBehaviour {

	private int num = 0;
	public TextMesh text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTouch() {
		num += 1;
		text.text = "Touch " + num;
	}

}
