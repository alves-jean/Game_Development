using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    private bool alreadyHit = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyHit) return;

        if (!other.CompareTag("Player")) return;

        alreadyHit = true;

        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager não encontrado na cena!");
            return;
        }

        GameManager.instance.LoseLife();

        Debug.Log("Tocou no perigo. Vidas: " + GameManager.instance.GetLives());

        if (GameManager.instance.GetLives() <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}