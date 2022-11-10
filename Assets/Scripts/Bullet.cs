using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 70f;
    public float explosionRadius = 0f;
    public GameObject impactEffectPrefab;
    private Transform targetTransform;
    private Vector3 dir;
    private float distanceThisFrame;

    public void Seek(Transform _targetTransform)
    {
        targetTransform = _targetTransform;
    }

    private void Update()
    {
        if (targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }
        dir = targetTransform.position - transform.position;
        distanceThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(targetTransform);
    }

    private void HitTarget()
    {
        GameObject impactEffectGameObject = (GameObject)Instantiate(impactEffectPrefab, transform.position, transform.rotation);
        Destroy(impactEffectGameObject, impactEffectGameObject.GetComponent<ParticleSystem>().main.startLifetime.constant + 3f);
        Destroy(gameObject);
        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(targetTransform);
        }
    }

    private void Damage(Transform enemyTransform)
    {
        enemyTransform.GetComponent<Enemy>().TakeDamage();
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }
}
