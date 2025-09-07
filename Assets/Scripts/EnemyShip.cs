using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [Header("Code For Patroling")]
    public int CurrentWayPoint = 0;
    public float shipSpeed;
    public float reachDistance = 0.4f;
    public List<Transform> wayPoints = new List<Transform>();
    float distance;
    float rotationSpeed = 5f;
    public Transform shootPoint;
    public float shootRate = 1f;    // fire delay (seconds)
    private float shootCooldown = 0f;
    public GameObject enemyBullet;


    [Header("HealthBar")]
    public HealthBar healthBar;
    public int MaxHealth;
    public int CurrentHealth;
    public GameObject blastA;

    [Header("Follow the player")]
    public GameObject Player;
    public float radarRange;

    public enum EnemyStates
    {
        ON_Path,
        Fight
    }
    public EnemyStates enemyState;

    void Start()
    {
        CurrentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
        Player = GameObject.FindGameObjectWithTag("Player");
        enemyState = EnemyStates.ON_Path;

        //temp test
        //TakeDamage(50);
    }

    void Update()
    {
        // Cooldown timer update
        if (shootCooldown > 0)
            shootCooldown -= Time.deltaTime;

        FindThePlayer();

        switch (enemyState)
        {
            case EnemyStates.ON_Path:
                MoveEnemyShip();
                break;

            case EnemyStates.Fight:
                ChasePlayer();
                break;
        }
    }

    void MoveEnemyShip()
    {
        distance = Vector3.Distance(wayPoints[CurrentWayPoint].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[CurrentWayPoint].position, shipSpeed * Time.deltaTime);

        Vector3 dir = wayPoints[CurrentWayPoint].position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle - 90), rotationSpeed * Time.deltaTime);

        if (distance < reachDistance)
        {
            CurrentWayPoint++;
        }
        if (CurrentWayPoint >= wayPoints.Count)
        {
            CurrentWayPoint = 0;
        }
    }

    void FindThePlayer()
    {
        float findDistanceToPlayer = Vector2.Distance(transform.position, Player.transform.position);

        if (findDistanceToPlayer <= radarRange)
        {
            enemyState = EnemyStates.Fight;
            Debug.Log("Player detected!");
        }
        else
        {
            enemyState = EnemyStates.ON_Path;
        }
    }

    void ChasePlayer()
    {
        Vector3 dir = Player.transform.position - transform.position;

        // Move towards player
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, shipSpeed * Time.deltaTime);

        // Rotate towards player
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle - 90), rotationSpeed * Time.deltaTime);

        // Shoot if cooldown is over
        if (shootCooldown <= 0f)
        {
            Instantiate(enemyBullet, shootPoint.position, shootPoint.rotation);
            shootCooldown = shootRate; // reset cooldown
        }
    }
    public void TakeDamage(int damageValue)
    {
        CurrentHealth -= damageValue;
        if (CurrentHealth <= 0)
        {
            Instantiate(blastA, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            healthBar.SetValue(CurrentHealth);
        }

    }
}
