﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour {

	public Camera mainCamera;
    
	public static GameObject selectedNode;

	// Use this for initialization
	void Start () {
		Input.simulateMouseWithTouches = true;
        selectedNode = null;
	}

    bool ignore = false;

	// Update is called once per frame
	void Update () {
        if (EventSystem.current.IsPointerOverGameObject()) {
            ignore = true;
			return;
        }
		List<Touch> touches = InputHelper.GetTouches();
        if (touches.Count > 0) {
            if (EventSystem.current.IsPointerOverGameObject(touches[0].fingerId)) {
                //Debug.Log("gg");
                ignore = true;
                return;
            }
            if (touches[0].phase == TouchPhase.Ended) {
                if (ignore) {
                    ignore = false;
                    return;
                }
                Ray rayCast = mainCamera.ScreenPointToRay(touches[0].position);
                RaycastHit raycastHit;
                if (Physics.Raycast(rayCast, out raycastHit)) {
                    //Debug.LogFormat("Touch at {0}", raycastHit.collider.name);
                    raycastHit.collider.SendMessage("OnTouch");
                    if (selectedNode && !raycastHit.collider.name.Contains("Node")) {
                        selectedNode.GetComponent<Node>().Cancel();
                    }
                } else {
                    if (selectedNode) {
                        selectedNode.GetComponent<Node>().Cancel();
                    }
                }
            }
		}
	}
}
