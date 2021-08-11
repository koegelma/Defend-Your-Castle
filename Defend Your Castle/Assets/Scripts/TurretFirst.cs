using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFirst : MonoBehaviour
{
    [Header("Variable Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup")]
    public string enemyTag = "Enemy";
    private Transform target;
    public Transform headRotation;
    private float turnSpeed = 8f;
    private float longestDistTravelled;
    private float distance;
    private GameObject nearestEnemy;
    public GameObject bulletPrefab;
    public Transform firePoint;


    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(headRotation.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        headRotation.rotation = Quaternion.Euler(0f, rotation.y, 0f);


        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        longestDistTravelled = 0f;
        nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            FirstEnemy(enemy);
        }

        if (nearestEnemy != null && distance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void FirstEnemy(GameObject enemy)
    {
        float distTravelled = enemy.GetComponent<EnemyMovement>().distanceTravelled;
        float distToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

        if (distTravelled > longestDistTravelled && distToEnemy <= range)
        {
            longestDistTravelled = distTravelled;
            nearestEnemy = enemy;
            distance = distToEnemy;
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
