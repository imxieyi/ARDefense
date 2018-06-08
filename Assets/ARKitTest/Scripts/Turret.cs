﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    Transform target;

    [Header("Attributes")]
	
	public float range = 15f;   
    public float fireRate = 1f;
    private float fireCountdown = 0f;

	[Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

	public Transform rotateBase;
    public float rotateSpeed = 20f;

	public GameObject bulletPrefab;
	public Transform firePoint;
    Transform spawnBase;

    float baseScale;

	// Use this for initialization
	void Start () {
		spawnBase = GameObject.Find("Game Base").transform;
		baseScale = spawnBase.localScale.x;
		range *= baseScale;
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
		} else {
			target = null;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			fireCountdown -= Time.deltaTime;
			if (fireCountdown < 0) {
				fireCountdown = 0;
			}
			return;
		}
        
        // Target lock on
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(rotateBase.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
		rotateBase.rotation = Quaternion.Euler(0f, rotation.y, 0f);

		if (fireCountdown <= 0) {
			Shoot();
			fireCountdown = 1f / fireRate;
		}

		fireCountdown -= Time.deltaTime;
	}

	void Shoot() {
		var bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, spawnBase);
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
