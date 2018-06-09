using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {
    
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

    public void ShowActionButtons() {
        sellItem.SetActive(true);
        upgradeItem.SetActive(true);
        laserBeamerItem.SetActive(false);
        missileLauncherItem.SetActive(false);
        standardTurretItem.SetActive(false);
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

}
