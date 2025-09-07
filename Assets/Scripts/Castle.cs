using UnityEngine;

public class Castle : MonoBehaviour
{

    public int castlePower;
    int bulletPower = 5;
    public GameObject blastB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        if (castlePower > 0)
        {
            castlePower -= bulletPower;
        }
        else
        {
            GameObject blast = Instantiate(blastB, transform.position, transform.rotation);
            Destroy(blast, 1f);
            Destroy(gameObject);
        }
    }
}
