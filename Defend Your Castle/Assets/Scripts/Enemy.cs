using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    
    [HideInInspector]
    public float speed;

    public float health = 100;
    public int moneyGain = 10;
    public int damageToPlayer = 1;
    public GameObject deathEffect;

    private void Start()
    {
        speed = startSpeed;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
            PlayerStats.Money += moneyGain;
        }
    }

    public void Slow(float amount)
    {
        speed = startSpeed * (1f - amount);
    }

    public void Die()
    {
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
