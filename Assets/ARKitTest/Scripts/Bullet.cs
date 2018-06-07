using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	Transform target;

	public float speed = 70f;
	public GameObject impactEffect;
    Transform spawnBase;
    float baseScale;

	public void SetTarget(Transform target) {
		this.target = target;
        spawnBase = GameObject.Find("Game Base").transform;
        baseScale = spawnBase.localScale.x;
	}

	// Update is called once per frame
	void Update() {
		if (target == null) {
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime * baseScale;

		if (dir.magnitude <= distanceThisFrame) {
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
	}

	void HitTarget() {
		var effect = Instantiate(impactEffect, transform.position, transform.rotation, spawnBase);
		Destroy(effect, 2f);
		Destroy(target.gameObject);
		Destroy(gameObject);
	}
}
