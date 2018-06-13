using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour {

    public PlaneManager planeManager;
    public Transform baseTrans;

    // Update is called once per frame
	void Update () {
        if (float.IsNaN(planeManager.baseRotation)) {
            return;
        }
        var dir = transform.position - baseTrans.position;
        dir = Quaternion.Euler(0, planeManager.baseRotation, 0) * dir;
        transform.position = baseTrans.position + dir;
	}

}
