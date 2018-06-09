using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	public Color hightlightColor;
	public Vector3 positionOffset;

    public GameObject nodeUI;

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
			//Cancel();
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
        if (turret) {
            GameObject nodeUIObj = Instantiate(nodeUI, turret.transform.position + new Vector3(0, 2.5f * GameBase.scale), Quaternion.identity, GameBase.trans);
            var t = turret.GetComponent<Turret>();
            t.nodeUI = nodeUIObj.GetComponent<NodeUI>();
            Shop.instance.ShowActionButtons(t.type);
        } else {
            Shop.instance.ShowBuildButtons();
        }
	}

	public Vector3 GetBuildPosition() {
		return transform.position + positionOffset * GameBase.scale;
	}

	public bool BuildTurret(GameObject prefab) {
        if (turret) {
			return false;
        }
        Cancel();
        turret = Instantiate(prefab, GetBuildPosition(), transform.rotation, GameBase.trans);
		return true;
	}

	public void Cancel() {
		TouchManager.selectedNode = null;
		rend.material.color = defaultColor;
        if (turret) {
            turret.GetComponent<Turret>().nodeUI.GetComponent<Animator>().SetBool("UIOut", true);
            Destroy(turret.GetComponent<Turret>().nodeUI.gameObject, 1f);
        }
        Shop.instance.DeactiveButtons();
	}

}
