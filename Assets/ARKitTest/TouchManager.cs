using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {

	public Camera mainCamera;

	// Use this for initialization
	void Start () {
		Input.simulateMouseWithTouches = true;
	}
	
	// Update is called once per frame
	void Update () {
		List<Touch> touches = InputHelper.GetTouches();
		if (touches.Count > 0 && touches[0].phase == TouchPhase.Ended) {
			Ray rayCast = mainCamera.ScreenPointToRay(touches[0].position);
			RaycastHit raycastHit;
			if (Physics.Raycast(rayCast, out raycastHit)) {
				Debug.LogFormat("Touch at {0}", raycastHit.collider.name);
				raycastHit.collider.SendMessage("OnTouch");
			}
		}
	}
}
