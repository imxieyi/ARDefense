using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAtPlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(GetComponent<MeshRenderer>());
		Destroy(GetComponent<MeshFilter>());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
