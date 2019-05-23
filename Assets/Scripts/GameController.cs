using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int     difficulty, playerLives;
    public float   startDelay, spawnDelay, waveDelay;
    public Vector3 spawnValues;

    public GameObject[] hazards;

    public Text scoreText, highScoreText;
    public Text powerupText, powerupTime;
    public Text livesText, waveText;
    public Text gameOverText, restartText;
    public bool gameOver;

    private int score, highScore;
    private int waveCount, hazardsOnScreen;
    private int hazardVariety, hazardFrequency;

    private GameObject player;
    private Vector3    playerSpawn;


    void Start()
    {
        player      = GameObject.FindWithTag("Player");
        playerSpawn = player.transform.position;

        gameOver = false;

        Cursor.visible   = false;
        Cursor.lockState = CursorLockMode.Locked;

        SetDifficulty();

        livesText.text    = "Lives: " + playerLives;
        waveText.text     =
        powerupText.text  =
        powerupTime.text  = 
        restartText.text  =
        gameOverText.text = "";

        waveCount =
        score     = 0;
        highScore = PlayerPrefs.GetInt("highScore", 0);
        UpdateScore();

        StartCoroutine(SpawnWaves());
    }


    void SetDifficulty()
    {
        difficulty = PlayerPrefs.GetInt("difficulty", 1);

        if (difficulty < 1) {
            difficulty = 1;
        }

        if (difficulty > 3) {
            difficulty = 3;
        }

        playerLives    -= (difficulty - 1);
        hazardVariety   = difficulty;
        hazardFrequency = (hazards.Length + 1) - difficulty;
        waveDelay      /= difficulty;

        // Debug.Log("difficulty: " + difficulty);
        // Debug.Log("hazardFrequency: " + hazardFrequency);
        // Debug.Log("waveDelay: " + waveDelay);
    }


    void Update()
    {
        if (gameOver) {
            PlayerPrefs.SetInt("highScore", highScore);

            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene("Main");
            }

            if (Input.GetButton("Cancel")) {
                gameOverText.text = "Thanks for playing!";
                restartText.text  = "";
                new WaitForSeconds(5);
                Application.Quit();
            }
        }
    }


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startDelay);

        while (true) {

            if (gameOver) {
                break;
            }

            waveCount ++;
            yield return new WaitForSeconds(waveDelay);

            waveText.text   = "Wave: " + waveCount;
            hazardsOnScreen = difficulty + (waveCount * difficulty);

            for (int i = 0; i < hazardsOnScreen; i++) {
                SpawnHazard(hazardVariety);
                yield return new WaitForSeconds(spawnDelay);
            }

            if (hazardVariety < (hazards.Length + 1) && ((waveCount % hazardFrequency) == 0)) {
                hazardVariety ++;
            }

            // Debug.Log("hazardVariety: " + hazardVariety);
            // Debug.Log("hazardsOnScreen: " + hazardsOnScreen);
        }
    }


    void SpawnHazard(int hazardVariety)
    {
        GameObject hazard = hazards[Random.Range(0, hazardVariety)];

        float      spawnRandomX  = Random.Range(-spawnValues.x, spawnValues.x);
        Vector3    spawnPosition = new Vector3(spawnRandomX, spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;

        Instantiate(hazard, spawnPosition, spawnRotation);
    }


    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;

        if (score > highScore) {
            highScore = score;
        }

        UpdateScore();
    }


    void UpdateScore()
    {
        scoreText.text     = "Your Score: " + score;
        highScoreText.text = "High Score: " + highScore;
    }

    public void UpdateLives()
    {
        playerLives --;
        livesText.text = "Lives: " + playerLives;

        player.transform.position = playerSpawn;
    }

    public void GameOver()
    {
        gameOver = true;

        livesText.text   =
        waveText.text    =
        powerupText.text = 
        powerupTime.text = "";

        gameOverText.text = "Game Over";
        restartText.text  = "Press 'esc' to quit or 'R' to restart";
    }

}
