using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	public Color hightlightColor;
	public Vector3 positionOffset;

	Color defaultColor;
	Renderer rend;
    Transform spawnBase;
    float baseScale;

	GameObject turret;

	// Use this for initialization
	void Start () {
		rend = GetComponent<MeshRenderer>();
		defaultColor = rend.material.color;
        spawnBase = GameObject.Find("Game Base").transform;
        baseScale = spawnBase.localScale.x;
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

	public void BuildTurret(GameObject prefab) {
        if (turret) {
			return;
        }
		turret = Instantiate(prefab, transform.position + positionOffset * baseScale, transform.rotation, spawnBase);
	}

	public void Cancel() {
		TouchManager.selectedNode = null;
		rend.material.color = defaultColor;
	}

}
