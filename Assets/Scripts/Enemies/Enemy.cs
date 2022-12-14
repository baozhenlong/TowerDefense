using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    public float startHealth = 100f;
    public int worth = 50;
    public GameObject deathEffectPrefab;
    [HideInInspector]
    public float speed;
    private float health;
    private bool isDead = false;

    [Header("Unity Stuff")]
    public Image healthBarImg;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
        healthBarImg.fillAmount = 1;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBarImg.fillAmount = health / startHealth;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        PlayerStats.Currency += worth;
        GameObject deathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        Destroy(deathEffect, deathEffect.GetComponent<ParticleSystem>().main.startLifetime.constant + 3f);
        Destroy(gameObject);
        WaveSpawner.AliveEnemies -= 1;
    }

    public void Slow(float value)
    {
        speed = startSpeed * (1 - value);
    }
}
