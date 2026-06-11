using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RestartGame()
    {
        GameManager.instance.ResetLives();

        GameManager.score = 0;
        GameManager.diamondScore = 0;

        SceneManager.LoadScene("Menu");
    }
}