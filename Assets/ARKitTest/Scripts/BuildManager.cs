using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

	public static BuildManager instance;

	void Awake() {
		if (instance != null) {
			Debug.LogError("More than one BuildManager in scene!");
			return;
		}
		instance = this;
	}

	public GameObject standardTurretPrefab;
    public GameObject missileLauncherPrefab;
    
	public void BuildTurret(GameObject prefab) {
		if (TouchManager.selectedNode) {
			TouchManager.selectedNode.GetComponent<Node>().BuildTurret(prefab);
		}
	}

}
