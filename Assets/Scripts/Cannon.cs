using UnityEngine;

public class Cannon : MonoBehaviour
{
    private Transform player;   // public ki zarurat nahi
    private Vector3 offset;
    public GameObject playerBullet;
    public Transform shootPoint;


    public Joystick cannonJoystick;   // <- yaha apna right side joystick drag karna
    public float fireRate = 0.25f;    // kitni speed se goli chale
    private float nextFireTime = 0f;

    void Start()
    {
        // Auto find Player by name
        player = GameObject.Find("Player").transform;


        offset = transform.position - player.position;
    }

    void Update()
    {
        // Cannon hamesha Player ke saath chipka rahega
        transform.position = player.position + offset;


        // Joystick input check
        float h = cannonJoystick.Horizontal;
        float v = cannonJoystick.Vertical;

        if (h != 0 || v != 0)   // agar fire joystick move kiya
        {
            // Cannon ko aim karwao
            float angle = Mathf.Atan2(v, h) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            // Fire bullets with delay
            if (Time.time > nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot()
    {
        Instantiate(playerBullet, shootPoint.position, shootPoint.rotation);
    }
}