using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI highScoreText; //GameOver Screen HighScore
    public TextMeshProUGUI highScoreTextTitleScreen; //TitleScreen HighScpre

    public bool isGameOver = false;
    public bool mouseControl = false;

    public GameObject gameOverScreen;
    public GameObject titleScreen;
    public GameObject menuScreen;
    public GameObject gameInfo;
    public GameObject startGame;
    public GameObject backgroundParticle;

    public GameObject levelPrefab;

    public GameObject mouseTurnOnButton;
    public GameObject mouseTurnOffButton;

    private int score = 0;
    private int lives = 1;
    private int brickCount = 0;
    private int level = 1;
    private int highScore = 0;
    private bool startScreen = true; // TitleScreen

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
        levelText.text = "Level: " + level;

        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreTextTitleScreen.text = "HighScore: " + highScore;

        // If the restart button is pressed then after loading the scence and the game will start directly
        if (PlayerPrefs.GetInt("Restart") == 1)
        {
            StartGame();
            PlayerPrefs.SetInt("Restart", 0);
        }

        // Player play of style 1 for mouse and 0 for keyboards arrows
        if(PlayerPrefs.GetInt("mouseControl") == 1)
        {
            turnOnMouseControl();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // GameOver or when in TitleScreen
        if(isGameOver || startScreen)
        {
            return;
        }

        // Finding the brick count and loading next level if no.of bricks are zero.
        brickCount = GameObject.FindGameObjectsWithTag("Brick").Length;
        if(brickCount == 0)
        {
            NextLevel();
        }
    }

    // Score Update
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    // Lives Update.
    public void UpdateLives(int livesToAdd)
    {
        lives += livesToAdd;
        // If lives count is zero. Showing GameOver Sceen
        if(lives<=0)
        {
            GameOver();
        }
        livesText.text = "Lives: " + lives;
    }

    // Lives Count
    public int GetLives()
    {
        return lives;
    }

    // Loading next level
    public void NextLevel()
    {
        Instantiate(levelPrefab);
        level += 1;
    }

    // GameOver Screen and updating Highest Score
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        isGameOver = true;
        updateHighScore();
        highScoreText.text = "HighScore: " + highScore;
    }

    // HighScore Update
    public void updateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    // OnClick of RestartButton in GameOver Screen
    public void RestartGame()
    {
        PlayerPrefs.SetInt("Restart", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Onclick of HomeButton in GameOver Screen
    public void HomeScreen()
    {
        PlayerPrefs.SetInt("Restart", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // StartGame Script
    public void StartGame()
    {
        startScreen = false;
        score = 0;
        UpdateScore(0);
        titleScreen.SetActive(false);
        startGame.SetActive(true);
        gameInfo.SetActive(true);
        backgroundParticle.SetActive(false);
    }

    // Closing the Game
    public void QuitGame()
    {
        PlayerPrefs.SetInt("Restart", 0);
        Application.Quit();
    }

    // MenuScreen where the user can change play of style
    public void MenuScreen()
    {
        menuScreen.SetActive(true);
        titleScreen.SetActive(false);
    }

    // onClick of BackToHome button in Menu Screen
    public void backToHomeScreen()
    {
        menuScreen.SetActive(false);
        titleScreen.SetActive(true);
    }

    // MenuScreen onClick of Yes for Mouse control
    public void turnOnMouseControl()
    {
        mouseControl = true;
        PlayerPrefs.SetInt("mouseControl", 1);
        mouseTurnOnButton.SetActive(true);
        mouseTurnOffButton.SetActive(false);
    }

    //MenuScreen onClick of No for Mouse control
    public void turnOffMouseControl()
    {
        mouseControl = false;
        PlayerPrefs.SetInt("mouseControl", 0);
        mouseTurnOffButton.SetActive(true);
        mouseTurnOnButton.SetActive(false);
    }
}
