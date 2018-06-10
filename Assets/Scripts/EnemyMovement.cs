using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

    Transform target;
    Transform map;
    int waypointIndex = 0;

    Enemy enemy;

    void Start() {
        enemy = GetComponent<Enemy>();
        map = GameObject.Find("Map").transform;
        target = Waypoints.points[0];
        target.TransformPoint(map.position);
    }

    void Update() {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime * GameBase.scale, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f * GameBase.scale) {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;
    }

    void GetNextWaypoint() {
        if (waypointIndex >= Waypoints.points.Length - 1) {
            EndPath();
            return;
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex];
        target.TransformPoint(map.position);
    }

    void EndPath() {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }

}
