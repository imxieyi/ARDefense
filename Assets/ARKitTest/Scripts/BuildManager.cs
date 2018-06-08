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

	public GameObject buildEffect;
    
	public void BuildTurret(TurretBlueprint blueprint) {
		if (TouchManager.selectedNode && PlayerStats.Money >= blueprint.cost) {
			var node = TouchManager.selectedNode.GetComponent<Node>();
			if (node.BuildTurret(blueprint.prefab)) {
				PlayerStats.Money -= blueprint.cost;

				GameObject effect = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity, GameBase.trans);
				Destroy(effect, 5f);

				Debug.Log("Money: " + PlayerStats.Money);
			}
		}
	}

}
