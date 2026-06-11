using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;

    private bool alreadyHit = false;

    public Transform pointA;
    public Transform pointB;

    public GameManager gameManager;

    private Transform target;
    private Rigidbody2D rb;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        target = pointB;
    }

    void FixedUpdate()
    {
        if (pointA == null || pointB == null) return;

        Vector2 newPosition = Vector2.MoveTowards(
            rb.position,
            target.position,
            speed * Time.fixedDeltaTime
        );

        rb.MovePosition(newPosition);

        if (Vector2.Distance(rb.position, target.position) < 0.1f)
        {
            target = target == pointA ? pointB : pointA;
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;

        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();

        if (sprite != null)
        {
            sprite.flipX = !sprite.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (alreadyHit) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            alreadyHit = true;

            GameManager.instance.LoseLife();

            if (GameManager.instance.GetLives() <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                SceneManager.LoadScene(
                    SceneManager.GetActiveScene().name
                );
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        GameManager.score += 50;
    }
}