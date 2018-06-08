using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed = 10f;
    
	Transform target;
	Transform map;
	int waypointIndex = 0;

	void Start() {
        map = GameObject.Find("Map").transform;
		target = Waypoints.points[0];
		target.TransformPoint(map.position);
	}

	void Update() {
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime * GameBase.scale, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= 0.4f * GameBase.scale) {
			GetNextWaypoint();
		}
	}

	void GetNextWaypoint() {
		if (waypointIndex >= Waypoints.points.Length - 1) {
			Destroy(gameObject);
			return;
		}

		waypointIndex++;
		target = Waypoints.points[waypointIndex];
        target.TransformPoint(map.position);
	}

}
