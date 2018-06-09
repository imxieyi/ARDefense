﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    Transform target;
    Enemy targetEnemy;

    [Header("General")]

    public float range = 15f;

    [Header("Use Bullets (default)")]

    public GameObject bulletPrefab;
    public float fireRate = 1f;
    float fireCountdown = 0f;

    [Header("Use Laser")]
    
    public bool useLaser = false;
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

	// Use this for initialization
	void Start () {
        if (useLaser) {
            lineRenderer.useWorldSpace = true;
            lineRenderer.enabled = false;
            impactEffect.Stop();
            impactLight.enabled = false;
        }
		range *= GameBase.scale;
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
