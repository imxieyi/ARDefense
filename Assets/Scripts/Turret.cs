﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    Transform target;
    Enemy targetEnemy;

    [Header("General")]

    [HideInInspector]
    public float range = 15f;
    public TurretType type;

    [Header("Use Bullets (default)")]

    public GameObject bulletPrefab;
    [HideInInspector]
    public float fireRate = 1f;
    float fireCountdown = 0f;

    [Header("Use Laser")]
    
    public bool useLaser = false;
    [HideInInspector]
    public int damageOverTime = 30;
    public float slowPercent = 0.5f;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

	[Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

	public Transform rotateBase;
    public float rotateSpeed = 20f;
	public Transform firePoint;

    [HideInInspector]
    public NodeUI nodeUI;

    [HideInInspector]
    public TurretBlueprint blueprint;

    int damage;
    float realRange;
    int level = 0;
    float sumValue = 0;

	// Use this for initialization
	void Start () {
        //Debug.Log("start");
        if (useLaser) {
            lineRenderer.useWorldSpace = true;
            lineRenderer.enabled = false;
            impactEffect.Stop();
            impactLight.enabled = false;
        }
		InvokeRepeating("UpdateTarget", 0f, 0.2f);
	}

	void UpdateTarget() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies) {
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance) {
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range) {
			target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
		} else {
			target = null;
            targetEnemy = null;
		}
	}
	
	// Update is called once per frame
    void Update () {

        if (nodeUI) {
            UpdateNodeUI();
        }

		if (target == null) {
            if (useLaser) {
                if (lineRenderer.enabled) {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            } else {
                fireCountdown -= Time.deltaTime;
                if (fireCountdown < 0) {
                    fireCountdown = 0;
                }
            }
			return;
		}

        LockOnTarget();

        if (useLaser) {
            Laser();
        } else {
            if (fireCountdown <= 0) {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
	}

    void UpdateNodeUI() {
        nodeUI.levelText.text = "Lv." + level;
        nodeUI.rangeText.text = ((int)realRange).ToString();
        if (useLaser) {
            nodeUI.damageText.text = damageOverTime + "/s";
            nodeUI.speedText.text = "N/A";
        } else {
            nodeUI.damageText.text = damage.ToString();
            nodeUI.speedText.text = string.Format("{0:0.00}/s", fireRate);
        }
    }

    public int GetUpgradeCost() {
        if (level >= blueprint.levels.Length) {
            return 0;
        }
        return blueprint.levels[level].cost;
    }

    public int GetSellPrice() {
        return (int)Mathf.Round(sumValue * Shop.instance.sellDiscount);
    }

    public void UpgradeTurret() {
        var l = blueprint.levels[level];
        sumValue += l.cost;
        level++;
        realRange = l.range;
        range = realRange * GameBase.scale;
        if (useLaser) {
            damageOverTime = l.damage;
            lineRenderer.startWidth = lineRenderer.endWidth = 0.3f * l.scale * GameBase.scale;
        } else {
            var bullet = bulletPrefab.GetComponent<Bullet>();
            bullet.damage = l.damage;
            damage = l.damage;
            fireRate = l.speed;
        }
        transform.localScale = new Vector3(l.scale, l.scale, l.scale);
        if (nodeUI) {
            nodeUI.transform.position = transform.position + new Vector3(0, 2.5f * l.scale * GameBase.scale);
        }
    }

    void LockOnTarget() {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotateBase.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
        rotateBase.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser() {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPercent);

        if (!lineRenderer.enabled) {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized * GameBase.scale * 1.5f;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

	void Shoot() {
		var bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, GameBase.trans);
		Bullet bullet = bulletGO.GetComponent<Bullet>();

		if (bullet != null) {
			bullet.SetTarget(target);
		}
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}

}
