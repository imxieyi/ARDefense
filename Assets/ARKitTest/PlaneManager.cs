﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class PlaneManager : MonoBehaviour {
	
    public GameObject planePrefab;
    private UnityARAnchorManager unityARAnchorManager;
    private bool planePlaced = false;
	private GameObject gameBase;
	public Camera mainCamera;
	public UnityARCameraManager cameraManager;
	public bool enableARKit;
    
    // Use this for initialization
    void Start() {
		m_HitTransform = new GameObject().transform;
		if (enableARKit) {
            unityARAnchorManager = new UnityARAnchorManager();
            UnityARUtility.InitializePlanePrefab(planePrefab);
            gameBase = GameObject.Find("Game Base");
            gameBase.SetActive(false);
		} else {
			Destroy(cameraManager.gameObject);
			Destroy(mainCamera.gameObject.GetComponent<UnityARVideo>());
			Destroy(mainCamera.gameObject.GetComponent<UnityARCameraNearFar>());
		}
	}

    Transform m_HitTransform;
    public float maxRayDistance = 30.0f;
    public LayerMask collisionLayer = 1 << 10;  //ARKitPlane layer

	void PlacePlane() {
		gameBase.SetActive(true);
		gameBase.transform.position = m_HitTransform.position;
		gameBase.transform.rotation = m_HitTransform.rotation;
		planePlaced = true;
		// Turn off plane detection
		GameObject.Find("Camera Manager").GetComponent<UnityARCameraManager>().planeDetectionOFF();
		unityARAnchorManager.Destroy();
	}

    bool HitTestWithResultType(ARPoint point, ARHitTestResultType resultTypes) {
        List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface().HitTest(point, resultTypes);
        if (hitResults.Count > 0) {
            foreach (var hitResult in hitResults) {
                Debug.Log("Got hit!");
                m_HitTransform.position = UnityARMatrixOps.GetPosition(hitResult.worldTransform);
                m_HitTransform.rotation = UnityARMatrixOps.GetRotation(hitResult.worldTransform);
                Debug.Log(string.Format("x:{0:0.######} y:{1:0.######} z:{2:0.######}", m_HitTransform.position.x, m_HitTransform.position.y, m_HitTransform.position.z));
                return true;
            }
		} else {
			Debug.Log("Ggg");
		}
        return false;
	}

    void Update() {
		if (enableARKit && !planePlaced) {
#if UNITY_EDITOR   //we will only use this script on the editor side, though there is nothing that would prevent it from working on device
            if (Input.GetMouseButtonDown(0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                //we'll try to hit one of the plane collider gameobjects that were generated by the plugin
                //effectively similar to calling HitTest with ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent
                if (Physics.Raycast(ray, out hit, maxRayDistance, collisionLayer)) {
                    //we're going to get the position from the contact point
                    m_HitTransform.position = hit.point;
                    Debug.Log(string.Format("x:{0:0.######} y:{1:0.######} z:{2:0.######}", m_HitTransform.position.x, m_HitTransform.position.y, m_HitTransform.position.z));

                    //and the rotation from the transform of the plane collider
                    m_HitTransform.rotation = hit.transform.rotation;
                    PlacePlane();
                }
            }
#else
            if (Input.touchCount > 0 && m_HitTransform != null)
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
                {
                    var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
                    ARPoint point = new ARPoint {
                        x = screenPosition.x,
                        y = screenPosition.y
                    };

                    // prioritize reults types
                    ARHitTestResultType[] resultTypes = {
                        ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
                        // if you want to use infinite planes use this:
                        //ARHitTestResultType.ARHitTestResultTypeExistingPlane,
                        ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 
                        ARHitTestResultType.ARHitTestResultTypeFeaturePoint
                    }; 
                    
                    foreach (ARHitTestResultType resultType in resultTypes)
                    {
                        if (HitTestWithResultType (point, resultType))
						{
                            PlacePlane();
                            return;
                        }
                    }
                }
            }
#endif
        }
    }

    void OnDestroy() {
		if (enableARKit) {
            unityARAnchorManager.Destroy();
		}
    }

}
