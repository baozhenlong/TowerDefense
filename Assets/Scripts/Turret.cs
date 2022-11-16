using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform targetTransform;
    private Enemy targetComp;

    [Header("General")]
    public float range = 15f;
    [Header("Use Bullets(Default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public float damagePerSecond = 20f;
    public float slowValue = .5f;
    public LineRenderer lineRenderer;
    public ParticleSystem[] impactParticleSystems;
    public Light impactLight;
    private Vector3 laserDir;

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
            targetComp = targetTransform.GetComponent<Enemy>();
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
            if (useLaser && lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
                foreach (var impactParticleSystem in impactParticleSystems)
                {
                    impactParticleSystem.Stop();
                }
                impactLight.enabled = false;
            }
            return;
        }
        LockOnTarget();
        if (useLaser)
        {
            Laser();
        }
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
        targetComp.TakeDamage(damagePerSecond * Time.deltaTime);
        targetComp.Slow(slowValue);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            foreach (var impactParticleSystem in impactParticleSystems)
            {
                impactParticleSystem.Play();
            }
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, targetTransform.position);
        // 效果在敌人身后
        laserDir = firePoint.position - targetTransform.position;
        impactParticleSystems[0].transform.position = targetTransform.position + laserDir.normalized;
        impactParticleSystems[0].transform.rotation = Quaternion.LookRotation(laserDir);
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).GetComponent<Bullet>().Seek(targetTransform);
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
