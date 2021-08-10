using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretNearest : MonoBehaviour
{
    [Header("Variable Attributes")]
    public float range = 15f;
    public float fireRate = 2f;
    private float fireCountdown = 0f;

    [Header("Unity Setup")]
    public string enemyTag = "Enemy";
    private Transform target;
    public Transform headRotation;
    private float turnSpeed = 8f;
    private float shortestDistance;
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
        shortestDistance = Mathf.Infinity;
        nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            DistanceToEnemy(enemy);
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void DistanceToEnemy(GameObject enemy)
    {
        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
        if (distanceToEnemy < shortestDistance)
        {
            shortestDistance = distanceToEnemy;
            nearestEnemy = enemy;
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
