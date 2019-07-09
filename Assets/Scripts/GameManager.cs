using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    Player player;
    LevelGenerator levelGenerator;

    public TextMeshProUGUI scoreText;
    public int pillarIndex = 0;

    public GameObject pauseCanvas;
    public GameObject gameOverCanvas;
    public bool gameIsPaused = false;
    public bool gameIsOver = false;

    void Start()
    {
        player = FindObjectOfType<Player>();
        levelGenerator = FindObjectOfType<LevelGenerator>();

        Time.timeScale = 1;

        if (!player)
        {
            Debug.LogWarning("Player can't be found by Game Manager");
        }
        if (!levelGenerator)
        {
            Debug.LogWarning("Level Generator can't be found by Game Manager");
        }
        if (!scoreText)
        {
            Debug.LogWarning("Score Text is missing from Game Manager");
        }
        if (pauseCanvas)
        {
            pauseCanvas.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Pause canvas is missing from Game Manager");
        }
        if (gameOverCanvas)
        {
            gameOverCanvas.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Game Over canvas is missing from Game Manager");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameIsOver)
        {
            ManagePause();
        }
    }

    public void MoveToNextPillar(bool hasDied, float endOfStickXPosition)
    {
        pillarIndex++;

        if (hasDied)
        {
            StartCoroutine(player.MovePlayer(endOfStickXPosition, true));
        }
        else
        {
            float destinationX = levelGenerator.pillars[pillarIndex].transform.position.x;
            StartCoroutine(player.MovePlayer(destinationX, false));

            levelGenerator.GenerateNewPillar();
        }
    }

    public void UpdateScore()
    {
        scoreText.text = pillarIndex.ToString();
    }

    public void ManagePause()
    {
        gameIsPaused = !gameIsPaused;
        pauseCanvas.SetActive(gameIsPaused);
        Time.timeScale = gameIsPaused ? 0 : 1;
    }

    public void ManageGameOver()
    {
        gameOverCanvas.SetActive(true);
    }

}
