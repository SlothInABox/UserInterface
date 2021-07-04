using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    private float spawnRate = 1.0f;
    public bool isGameActive;

    public TextMeshProUGUI scoreText;
    private int score;

    public TextMeshProUGUI livesText;
    private int lives;

    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    public GameObject titleScreen;

    public GameObject pauseScreen;
    public bool paused;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Pause game when P key pressed
        if (Input.GetKeyDown(KeyCode.P) && !titleScreen.activeSelf && isGameActive)
        {
            ChangePaused();
        }
    }

    private IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void LoseLife()
    {
        lives -= 1;
        livesText.text = "Lives: " + lives;

        if (lives == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;

        isGameActive = true;
        StartCoroutine(SpawnTarget());

        score = 0;
        scoreText.text = "Score: " + score;

        lives = 3;
        livesText.text = "Lives: " + lives;
        
        titleScreen.SetActive(false);
    }

    private void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
