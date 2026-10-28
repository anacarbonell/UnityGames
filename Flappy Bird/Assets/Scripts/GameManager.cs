using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject birdPrefab;

    [SerializeField]
    private Spawner spawner;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject medalPrefab;

    [SerializeField]
    private GameObject titleCanvas;

    [SerializeField]
    private GameObject gameOverCanvas;

    [SerializeField]
    private GameObject scoreCanvas;

    [SerializeField]
    private TextMeshProUGUI scoreGameOverText;

    [SerializeField]
    private PauseManager pauseManager;

    private int score = 0;
    private float medalRowSpacing = 0.9f;
    private float medalColumnSpacing = 0.8f;
    private int medalsPerColumn = 9;
    private List<GameObject> medalList = new List<GameObject>();
    private bool isRunning = false;
    private Bird bird;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning && Keyboard.current[Key.Space].wasPressedThisFrame)
        {
            StartGame();
        }
    }

    public void EndGame()
    {
        isRunning = false;
        spawner.StopSpawning();

        foreach (GameObject medal in medalList)
        {
            Destroy(medal);
        }
        medalList.Clear();

        scoreGameOverText.text = String.Format("Has conseguido {0} puntos.", score);

        pauseManager.SetPauseActive(false);

        gameOverCanvas.SetActive(true);
        scoreCanvas.SetActive(false);
    }

    public void AddScore()
    {
        if (!isRunning)
        {
            return;
        }

        score++;
        scoreText.text = score.ToString();

        if (score % 5 == 0)
        {
            int columns = medalList.Count / medalsPerColumn;
            int medalCount = medalList.Count % medalsPerColumn;
            Vector2 medalPosition = medalPrefab.transform.position;

            medalPosition.x += columns * medalColumnSpacing;
            medalPosition.y -= medalCount * medalRowSpacing;
            GameObject medal = Instantiate(medalPrefab, medalPosition, Quaternion.identity);
            medalList.Add(medal);
            Time.timeScale += 0.1f;
            bird.SetInvincibility(true);
        }
    }

    private void StartGame()
    {
        isRunning = true;
        Time.timeScale = 1f;

        score = 0;
        scoreText.text = score.ToString();

        titleCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        scoreCanvas.SetActive(true);

        pauseManager.SetPauseActive(true);

        spawner.StartSpawning();
        bird = Instantiate(birdPrefab).GetComponent<Bird>();
    }
}
