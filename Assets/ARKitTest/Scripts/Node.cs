using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	public Color hightlightColor;
	public Vector3 positionOffset;

	Color defaultColor;
	Renderer rend;
    
    [Header("Optional")]
	public GameObject turret;

	// Use this for initialization
	void Start () {
		rend = GetComponent<MeshRenderer>();
		defaultColor = rend.material.color;
	}

	void OnTouch() {
		Debug.Log("Touched at " + name);
		if (TouchManager.selectedNode == gameObject) {
			Cancel();
			return;
		}
		Highlight();
	}

	void Highlight() {
		if (TouchManager.selectedNode) {
			TouchManager.selectedNode.GetComponent<Node>().Cancel();
		}
		TouchManager.selectedNode = gameObject;
		rend.material.color = hightlightColor;
	}

	public Vector3 GetBuildPosition() {
		return transform.position + positionOffset * GameBase.scale;
	}

	public bool BuildTurret(GameObject prefab) {
        if (turret) {
			return false;
        }
		turret = Instantiate(prefab, GetBuildPosition(), transform.rotation, GameBase.trans);
		return true;
	}

	public void Cancel() {
		TouchManager.selectedNode = null;
		rend.material.color = defaultColor;
	}

}
