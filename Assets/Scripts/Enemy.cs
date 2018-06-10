using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public float startSpeed = 10f;
    [HideInInspector]
	public float speed;

    public float startHealth = 100;
    float health;

    public int reward = 20;

    public GameObject deathEffect;

    public Image healthBar;
    public Transform healthBarBase;

    [HideInInspector]
    public bool died = false;

    void Start() {
        health = startHealth;
        speed = startSpeed;
    }

    void Update() {
        healthBarBase.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }

    public void TakeDamage(float amount) {
        health -= amount;
        if (health <= 0) {
            Die();
        }
        healthBar.fillAmount = health / startHealth;
    }

    public void Slow(float percent) {
        speed = startSpeed * (1f - percent);
    }

    void Die() {
        if (died) {
            return;
        }
        died = true;
        PlayerStats.Money += reward;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity, GameBase.trans);
        Destroy(effect, 5);

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
    }

}
