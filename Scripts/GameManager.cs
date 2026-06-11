using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] hearts;

    private int currentLives;
    private const int maxLives = 5;

    public static int diamondScore;
    public static int score;

    private TextMeshProUGUI diamondScoreText;
    private TextMeshProUGUI scoreText;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        currentLives = PlayerPrefs.GetInt("Lives", maxLives);
    }

    void Start()
    {
        FindUI();
        UpdateHearts();
    }

    void Update()
    {
        FindUI();

        if (diamondScoreText != null)
            diamondScoreText.text = diamondScore.ToString();

        if (scoreText != null)
            scoreText.text = score.ToString();

        UpdateHearts();
    }

    void FindUI()
    {
        GameObject diamondObj = GameObject.Find("DiamondScore");
        if (diamondObj != null)
            diamondScoreText = diamondObj.GetComponent<TextMeshProUGUI>();

        GameObject scoreObj = GameObject.Find("Score");
        if (scoreObj != null)
            scoreText = scoreObj.GetComponent<TextMeshProUGUI>();

        if (hearts == null || hearts.Length == 0 || hearts[0] == null)
        {
            hearts = new GameObject[5];

            hearts[0] = GameObject.Find("Life1");
            hearts[1] = GameObject.Find("Life2");
            hearts[2] = GameObject.Find("Life3");
            hearts[3] = GameObject.Find("Life4");
            hearts[4] = GameObject.Find("Life5");
        }
    }

    public void LoseLifeAndRestart()
    {
        currentLives--;

        if (currentLives < 0)
            currentLives = 0;

        PlayerPrefs.SetInt("Lives", currentLives);
        PlayerPrefs.Save();

        UpdateHearts();

        if (currentLives <= 0)
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

    public void LoseLife()
    {
        currentLives--;

        if (currentLives < 0)
            currentLives = 0;

        PlayerPrefs.SetInt("Lives", currentLives);
        PlayerPrefs.Save();

        UpdateHearts();
    }

    public void ResetLives()
    {
        currentLives = maxLives;

        PlayerPrefs.SetInt("Lives", currentLives);
        PlayerPrefs.Save();

        UpdateHearts();
    }

    public int GetLives()
    {
        return currentLives;
    }

    void UpdateHearts()
    {
        if (hearts == null || hearts.Length == 0)
            return;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
            {
                hearts[i].SetActive(i < currentLives);
            }
        }
    }
}