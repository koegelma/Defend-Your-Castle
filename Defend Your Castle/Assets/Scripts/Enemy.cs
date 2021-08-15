using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;
    public float speed = 10f;
    public int health = 100;
    public int moneyGain = 10;
    public int damageToPlayer = 1;
    public float distanceTravelled = 0f;
    public GameObject deathEffect;
    private Vector3 lastPosition;
    void Start()
    {
        target = Waypoints.waypoints[0];

        lastPosition = transform.position;
        //InvokeRepeating("LogTravelDistance", 0f, 2f);
    }

    private void Update()
    {
        TranslateEnemy();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
            PlayerStats.Money += moneyGain;
        }
    }

    private void Die()
    {
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }

    private void LogTravelDistance()
    {
        Debug.Log(gameObject.name + " Distance Traveled: " + distanceTravelled);
    }

    private void TranslateEnemy()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        if (Vector3.Distance(transform.position, target.position) <= 0.2f) GetNextWaypoint();
    }

    private void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.waypoints.Length - 1)
        {
            PlayerStats.Life -= damageToPlayer;
            Die();
            return;
        }
        wavepointIndex++;
        target = Waypoints.waypoints[wavepointIndex];
    }
}
