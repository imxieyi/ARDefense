using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	public Color hightlightColor;
	public Vector3 positionOffset;

    public GameObject nodeUI;
    public GameObject buildEffect;
    public GameObject sellEffect;

	Color defaultColor;
	Renderer rend;
    
    [HideInInspector]
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
            var t = turret.GetComponent<Turret>();
            GameObject nodeUIObj = Instantiate(nodeUI, turret.transform.position + new Vector3(0, 2.5f * t.transform.localScale.x * GameBase.scale), Quaternion.identity, GameBase.trans);
            t.nodeUI = nodeUIObj.GetComponent<NodeUI>();
            Shop.instance.ShowActionButtons(t.type, t);
        } else {
            Shop.instance.ShowBuildButtons();
        }
	}

	public Vector3 GetBuildPosition() {
		return transform.position + positionOffset * GameBase.scale;
    }

    public void BuildTurret(TurretBlueprint blueprint) {
        if (turret) {
            return;
        }
        if (PlayerStats.Money >= blueprint.cost) {
            var node = TouchManager.selectedNode.GetComponent<Node>();
            Cancel();
            turret = Instantiate(blueprint.prefab, GetBuildPosition(), transform.rotation, GameBase.trans);
            //Debug.Log("init");
            var t = turret.GetComponent<Turret>();
            t.blueprint = blueprint;
            t.UpgradeTurret();
            PlayerStats.Money -= blueprint.cost;

            GameObject effect = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity, GameBase.trans);
            Destroy(effect, 5f);
        }
	}

    public void UpgradeTurret() {
        if (!turret) {
            return;
        }
        var t = turret.GetComponent<Turret>();
        int cost = t.GetUpgradeCost();
        if (cost == 0) {
            // Max level
            return;
        }
        if (PlayerStats.Money >= cost) {
            var node = TouchManager.selectedNode.GetComponent<Node>();
            PlayerStats.Money -= cost;
            t.UpgradeTurret();

            GameObject effect = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity, GameBase.trans);
            Destroy(effect, 5f);

            Shop.instance.ShowActionButtons(t.type, t);
        }
    }

    public void SellTurret() {
        if (!turret) {
            return;
        }

        PlayerStats.Money += turret.GetComponent<Turret>().GetSellPrice();

        GameObject effect = Instantiate(sellEffect, TouchManager.selectedNode.GetComponent<Node>().GetBuildPosition(), Quaternion.identity, GameBase.trans);
        Destroy(effect, 5f);

        Cancel();

        Destroy(turret);
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
