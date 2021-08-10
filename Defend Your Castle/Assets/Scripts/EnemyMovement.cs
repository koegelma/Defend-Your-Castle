using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;
    public float speed = 10f;
    public float distanceTravelled = 0f;
    private Vector3 lastPosition;
    void Start()
    {
        target = Waypoints.waypoints[0];

        lastPosition = transform.position;
        //InvokeRepeating("LogTravelDistance", 0f, 2f);
    }

    void Update()
    {
        TranslateEnemy();
    }

    void LogTravelDistance()
    {
        Debug.Log(gameObject.name + " Distance Traveled: " + distanceTravelled);
    }

    void TranslateEnemy()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.waypoints.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = Waypoints.waypoints[wavepointIndex];
    }
}
