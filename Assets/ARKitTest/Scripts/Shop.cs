using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	public TurretBlueprint standardTurret;
	public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

	BuildManager buildManager;

	void Start() {
		buildManager = BuildManager.instance;
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
