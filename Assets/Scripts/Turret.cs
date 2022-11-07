using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform targetTransform;

    [Header("General")]
    public float range = 15f;
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup")]
    public string enemyTag = "Enemy";
    public Transform partToRotateTransform;
    public Transform firePoint;
    public float turnSpeed = 10f;
    public float updateTargetRate = 0.5f;

    [Header("Calculate")]
    private float shortestDistanceToEnemy;
    private GameObject nearestEnemy;
    private float distanceToEnemy;
    private Quaternion dirLookRotation;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, updateTargetRate);
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        shortestDistanceToEnemy = Mathf.Infinity;
        nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistanceToEnemy)
            {
                shortestDistanceToEnemy = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistanceToEnemy <= range)
        {
            targetTransform = nearestEnemy.transform;
        }
        else
        {
            targetTransform = null;
        }
    }

    private void Update()
    {
        if (targetTransform == null)
        {
            return;
        }
        LockOnTarget();
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        Debug.Log("Shoot");
    }

    private void LockOnTarget()
    {
        dirLookRotation = Quaternion.LookRotation(targetTransform.position - transform.position);
        partToRotateTransform.rotation = Quaternion.Euler(
            0f,
            Quaternion.Lerp(partToRotateTransform.rotation, dirLookRotation, Time.deltaTime * turnSpeed).eulerAngles.y,
            0f
        );
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
