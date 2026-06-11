using UnityEngine;

public class Diamond : MonoBehaviour
{
    public AudioClip collectSound;
    private AudioSource audioSource;
    private bool collected = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            collected = true;

            GameManager.diamondScore += 1;
            GameManager.score += 10;

            audioSource.PlayOneShot(collectSound);

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            Destroy(gameObject, collectSound.length);
        }
    }
}