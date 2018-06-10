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
    
	public void BuildTurret(TurretBlueprint blueprint) {
		if (TouchManager.selectedNode) {
            TouchManager.selectedNode.GetComponent<Node>().BuildTurret(blueprint);
		}
	}

    public void UpgradeTurret() {
        if (TouchManager.selectedNode) {
            TouchManager.selectedNode.GetComponent<Node>().UpgradeTurret();
        }
    }

}
