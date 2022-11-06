using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform targetPointTransform;

    private int pointIndex = 0;

    private Vector3 dir;
    private Enemy enemy;
    private float stopDistance = 0.4f;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        targetPointTransform = WayPoints.points[pointIndex];
    }

    private void Update()
    {
        dir = targetPointTransform.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);
        if (dir.magnitude <= stopDistance)
        {
            UpdateNextPoint();
        }
    }

    private void UpdateNextPoint()
    {
        if (pointIndex >= WayPoints.points.Length - 1)
        {
            ReachTheEnd();
            return;
        }
        pointIndex += 1;
        targetPointTransform = WayPoints.points[pointIndex];
    }

    private void ReachTheEnd()
    {
        Destroy(gameObject);
    }
}
