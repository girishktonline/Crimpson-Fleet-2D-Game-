using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float _horizantalInput = 0, _verticalInput = 0;
    public float moveSpeed;
    Rigidbody2D rb2d;
    public Joystick joystick;

    float x, y;

    public ParticleSystem particle1, particle2;

    //TorpedoButton Mechanism
    public GameObject torpedo;
    public GameObject torpedoButton;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        // Start me ensure particles band ho
        if (particle1.isPlaying) particle1.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        if (particle2.isPlaying) particle2.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    void Update()
    {
        GetPlayerInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 directionVector = new Vector3(_horizantalInput, _verticalInput, 0);
        rb2d.linearVelocity = directionVector.normalized * moveSpeed;

        if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            rb2d.linearVelocity = 0.1f * moveSpeed * new Vector3(x, y, 0);

            if (particle1.isPlaying)
                particle1.Stop(true, ParticleSystemStopBehavior.StopEmitting); // ✅ clear nahi karega

            if (particle2.isPlaying)
                particle2.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
        else
        {
            RotatePlayer();

            if (!particle1.isPlaying)
                particle1.Play();   // ✅ sirf tabhi play karega jab band ho

            if (!particle2.isPlaying)
                particle2.Play();
        }
    }

    void GetPlayerInput()
    {
        _horizantalInput = joystick.Horizontal;
        _verticalInput = joystick.Vertical;

        if (_horizantalInput != 0)
            x = _horizantalInput;

        if (_verticalInput != 0)
            y = _verticalInput;
    }

    void RotatePlayer()
    {
        float angle = Mathf.Atan2(_verticalInput, _horizantalInput) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            GameManager.instance.IncreaseCoin();
        }
        if (collision.gameObject.CompareTag("Diamond"))
        {
            GameManager.instance.IncreaseDiamond();
        }
    }
    public void ShootTorpedo()
    {
        Instantiate(torpedo, transform.position, Quaternion.identity);
    }
}
