using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	Transform target;

	public float speed = 70f;
	public float explosionRadius = 0f;
	public GameObject impactEffect;

	public void SetTarget(Transform target) {
		this.target = target;
        explosionRadius *= GameBase.scale;
	}

	// Update is called once per frame
	void Update() {
		if (target == null) {
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime * GameBase.scale;

		if (dir.magnitude <= distanceThisFrame) {
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);
	}

	void HitTarget() {
		var effect = Instantiate(impactEffect, transform.position, transform.rotation, GameBase.trans);
		Destroy(effect, 5f);
		if (explosionRadius > 0f) {
			Explode();
		} else {
			Damage(target);
		}
		Destroy(gameObject);
	}

	void Explode() {
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var collider in colliders) {
			if (collider.tag == "Enemy") {
				Damage(collider.transform);
			}
		}
	}

	void Damage(Transform enemy) {
        Destroy(enemy.gameObject);
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}

}
