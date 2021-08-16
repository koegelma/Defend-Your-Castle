using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    public float distanceTravelled = 0f;
    private Transform target;
    private int wavepointIndex = 0;
    private Enemy enemy;
    private Vector3 lastPosition;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.waypoints[0];

        lastPosition = transform.position;
        //InvokeRepeating("LogTravelDistance", 0f, 2f);
    }

    private void Update()
    {
        TranslateEnemy();
    }

    private void LogTravelDistance()
    {
        Debug.Log(gameObject.name + " Distance Traveled: " + distanceTravelled);
    }
    private void TranslateEnemy()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        if (Vector3.Distance(transform.position, target.position) <= 0.2f) GetNextWaypoint();

        enemy.speed = enemy.startSpeed;
    }

    private void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.waypoints.Length - 1)
        {
            PlayerStats.Life -= enemy.damageToPlayer;
            enemy.Die();
            return;
        }
        wavepointIndex++;
        target = Waypoints.waypoints[wavepointIndex];
    }
}
