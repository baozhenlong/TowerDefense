using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    private void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage()
    {
        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
