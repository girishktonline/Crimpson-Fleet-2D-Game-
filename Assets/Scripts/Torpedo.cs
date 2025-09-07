using UnityEngine;

public class Torpedo : MonoBehaviour
{
    public Transform target;
    public string enemyTag = "EnemyShip";
    public float range = 100;
    public float speed;
    Vector3 centerpos;
    float distanceFromCamera = 10f;
    public GameObject blastTorpedo;
    public int damage;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        centerpos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, distanceFromCamera));
    }

    void Update()
    {
        if (target != null)
        {
            // Move toward the target
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Rotate smoothly toward the target
            Vector3 dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle - 90), 10f * Time.deltaTime);
        }
        else
        {
            // Move toward center when no target
            transform.position = Vector2.MoveTowards(transform.position, centerpos, speed * Time.deltaTime);
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
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
}
