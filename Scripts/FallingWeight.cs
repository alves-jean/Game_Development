using UnityEngine;
using UnityEngine.SceneManagement;

public class FallingWeight : MonoBehaviour
{
    public float delayToFall = 0.3f;
    public float gravityScale = 4f;

    private Rigidbody2D rb;
    private bool hasFallen = false;
    private bool alreadyHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = gravityScale;
    }

    public void Drop()
    {
        if (hasFallen) return;

        hasFallen = true;

        Invoke(nameof(StartFalling), delayToFall);
    }

    void StartFalling()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
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
}