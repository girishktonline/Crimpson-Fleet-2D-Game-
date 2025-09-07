using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float bulletSpeed;
    Rigidbody2D rb;
    public GameObject BlastA;
    public int bulletPower = 20;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BlastEffects();
        }
        if (collision.gameObject.CompareTag("EnemyShip"))
        {
            collision.gameObject.GetComponent<EnemyShip>().TakeDamage(bulletPower);
            BlastEffects();
        }
    }

    void BlastEffects()
    {
        GameObject blast = Instantiate(BlastA, transform.position, transform.rotation);
        Destroy(blast, 1f);
        Destroy(gameObject);
        
    }
}
