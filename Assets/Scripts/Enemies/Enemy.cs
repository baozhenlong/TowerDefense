using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    public float startHealth = 100f;
    public GameObject deathEffectPrefab;
    [HideInInspector]
    public float speed;
    private float health;
    private bool isDead = false;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        GameObject deathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        Destroy(deathEffect, deathEffect.GetComponent<ParticleSystem>().main.startLifetime.constant + 3f);
        Destroy(gameObject);
        WaveSpawner.AliveEnemies -= 1;
    }
}
