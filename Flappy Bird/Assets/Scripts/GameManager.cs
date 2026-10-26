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
    private int medalsPerColumn = 8;
    private List<GameObject> medalList = new List<GameObject>();
    private bool isRunning = false;

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
        score++;
        scoreText.text = score.ToString();

        if (score % 5 == 0)
        {
            int medalCount = medalList.Count;
            Vector2 medalPosition = medalPrefab.transform.position;

            if (medalCount % medalsPerColumn == 0)
            {
                medalPosition.x += medalCount / medalsPerColumn * medalColumnSpacing;
                medalCount = 0;
            }

            medalPosition.y -= medalCount * medalRowSpacing;
            GameObject medal = Instantiate(medalPrefab, medalPosition, Quaternion.identity);
            medalList.Add(medal);
        }
    }

    private void StartGame()
    {
        isRunning = true;

        score = 0;
        scoreText.text = score.ToString();

        titleCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        scoreCanvas.SetActive(true);

        pauseManager.SetPauseActive(true);

        spawner.StartSpawning();
        Instantiate(birdPrefab);
    }
}
