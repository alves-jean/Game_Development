using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalSceneManager : MonoBehaviour
{
    private TextMeshProUGUI diamondScoreText;
    private TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = GameManager.score.ToString();
        diamondScoreText.text = GameManager.diamondScore.ToString();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}