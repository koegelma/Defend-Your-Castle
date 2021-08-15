using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    [Header("General")]
    public float range = 15f;

    [Header("Use Bullets")]
    public bool targetFirstEnemy = false;
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup")]
    public string enemyTag = "Enemy";
    private Transform target;
    public Transform headRotation;
    private float turnSpeed = 12f;
    private float shortestDistance;
    private float longestDistTravelled;
    private float distance;
    private GameObject nearestEnemy;
    public Transform firePoint;

    private void Start()
    {
        if (!targetFirstEnemy)
        {
            InvokeRepeating("UpdateTargetNearest", 0f, 0.5f);
            return;
        }
        if (targetFirstEnemy) InvokeRepeating("UpdateTargetFirst", 0f, 0.5f);
    }

    private void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();

        if (useLaser) Laser();
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    private void Laser()
    {
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 impactDir = firePoint.position - target.position;
        impactEffect.transform.rotation = Quaternion.LookRotation(impactDir);
        impactEffect.transform.position = target.position + impactDir.normalized * (target.localScale.z / 2);
    }

    private void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(headRotation.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        headRotation.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null) bullet.Seek(target);
    }

    private void UpdateTargetNearest()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        shortestDistance = Mathf.Infinity;
        nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        { DistanceToEnemy(enemy); }

        if (nearestEnemy != null && shortestDistance <= range) target = nearestEnemy.transform;
        else target = null;
    }

    private void DistanceToEnemy(GameObject enemy)
    {
        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
        if (distanceToEnemy < shortestDistance)
        {
            shortestDistance = distanceToEnemy;
            nearestEnemy = enemy;
        }
    }

    private void UpdateTargetFirst()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        longestDistTravelled = 0f;
        nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        { FirstEnemy(enemy); }

        if (nearestEnemy != null && distance <= range) target = nearestEnemy.transform;
        else target = null;
    }

    private void FirstEnemy(GameObject enemy)
    {
        float distTravelled = enemy.GetComponent<Enemy>().distanceTravelled;
        float distToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

        if (distTravelled > longestDistTravelled && distToEnemy <= range)
        {
            longestDistTravelled = distTravelled;
            nearestEnemy = enemy;
            distance = distToEnemy;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
