using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float startSpeed = 10f;
    [HideInInspector]
	public float speed;

    public float health = 100;

    public int reward = 20;

    public GameObject deathEffect;

    void Start() {
        speed = startSpeed;
    }

    public void TakeDamage(float amount) {
        health -= amount;
        if (health <= 0) {
            Die();
        }
    }

    public void Slow(float percent) {
        speed = startSpeed * (1f - percent);
    }

    void Die() {
        PlayerStats.Money += reward;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity, GameBase.trans);
        Destroy(effect, 5);

        Destroy(gameObject);
    }

}
