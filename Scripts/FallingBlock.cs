using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    public float delayToFall = 1f;

    private Rigidbody2D rb;
    private bool hasStartedFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStartedFalling) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            hasStartedFalling = true;
            Invoke(nameof(Fall), delayToFall);
        }
    }

    void Fall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}