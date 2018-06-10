using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public float sellDiscount = 0.6f;
    
    [Header("Turrets")]

	public TurretBlueprint standardTurret;
	public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

    [HideInInspector]
    public static Shop instance;

    [Header("Buttons")]

    public GameObject sellItem;
    public GameObject upgradeItem;
    public GameObject laserBeamerItem;
    public GameObject missileLauncherItem;
    public GameObject standardTurretItem;

    [Header("Sprites")]

    public Sprite standardTurretSprite;
    public Sprite missileLauncherSprite;
    public Sprite laserBeamerSprite;

	BuildManager buildManager;

    void Awake() {
        instance = this;
    }

    void Start() {
		buildManager = BuildManager.instance;
        standardTurretItem.GetComponentInChildren<Text>().text = "$" + standardTurret.cost;
        missileLauncherItem.GetComponentInChildren<Text>().text = "$" + missileLauncher.cost;
        laserBeamerItem.GetComponentInChildren<Text>().text = "$" + laserBeamer.cost;
	}

    public void DeactiveButtons() {
        sellItem.SetActive(false);
        upgradeItem.SetActive(false);
        laserBeamerItem.SetActive(false);
        missileLauncherItem.SetActive(false);
        standardTurretItem.SetActive(false);
    }

    public void ShowBuildButtons() {
        sellItem.SetActive(false);
        upgradeItem.SetActive(false);
        laserBeamerItem.SetActive(true);
        missileLauncherItem.SetActive(true);
        standardTurretItem.SetActive(true);
    }

    public void ShowActionButtons(TurretType type, Turret turret) {
        sellItem.SetActive(true);
        sellItem.GetComponentInChildren<Text>().text = "$" + turret.GetSellPrice();
        var cost = turret.GetUpgradeCost();
        upgradeItem.SetActive(true);
        if (cost > 0) {
            upgradeItem.GetComponentInChildren<Text>().text = "$" + cost;
        } else {
            upgradeItem.GetComponentInChildren<Text>().text = "Max";
        }
        laserBeamerItem.SetActive(false);
        missileLauncherItem.SetActive(false);
        standardTurretItem.SetActive(false);
        switch (type) {
            case TurretType.STANDARD:
                sellItem.GetComponent<Image>().sprite = standardTurretSprite;
                upgradeItem.GetComponent<Image>().sprite = standardTurretSprite;
                break;
            case TurretType.MISSILE:
                sellItem.GetComponent<Image>().sprite = missileLauncherSprite;
                upgradeItem.GetComponent<Image>().sprite = missileLauncherSprite;
                break;
            case TurretType.LASER:
                sellItem.GetComponent<Image>().sprite = laserBeamerSprite;
                upgradeItem.GetComponent<Image>().sprite = laserBeamerSprite;
                break;
        }
    }

    public void PurchaseStandardTurret() {
		buildManager.BuildTurret(standardTurret);
	}

	public void PurchaseMissileLauncher() {
		buildManager.BuildTurret(missileLauncher);
    }

    public void PurchaseLaserBeamer() {
        buildManager.BuildTurret(laserBeamer);
    }

    public void UpgradeTurret() {
        buildManager.UpgradeTurret();
    }

    public void SellTurret() {
        buildManager.SellTurret();
    }

}
