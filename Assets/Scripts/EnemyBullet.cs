using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float bulletSpeed;
    Rigidbody2D rb;
    public GameObject BlastA;

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
        if (collision.gameObject.CompareTag("Player"))
        {
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
