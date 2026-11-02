using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject heartPrefab;

    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private GameObject paddlePrefab;

    private UserDataManager userDataManager;
    private int currentScene;

    private List<Heart> hearts = new List<Heart>();
    private GameObject paddle;
    private int bricksLeft;

    void Awake()
    {
        userDataManager = UserDataManager.Instance;
        currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 1)
        {
            userDataManager.ResetLives();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bricksLeft = GameObject.FindGameObjectsWithTag("Brick").Length;

        for (int i = 0; i < userDataManager.MaxLives; i++)
        {
            Vector2 position = heartPrefab.transform.position;
            position.x += 0.5f * i;
            Heart heart = Instantiate(heartPrefab, position, Quaternion.identity)
                .GetComponent<Heart>();

            if (i >= userDataManager.Lives)
            {
                heart.UpdateSprite(true);
            }
            hearts.Add(heart);
        }

        InstantiatePrefabs();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[Key.L].wasPressedThisFrame)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
    }

    public void LoseLive()
    {
        Destroy(paddle);
        userDataManager.LoseLive();
        for (int i = hearts.Count; i > userDataManager.Lives; i--)
        {
            hearts[i - 1].UpdateSprite(true);
        }

        if (userDataManager.Lives > 0)
        {
            InstantiatePrefabs();
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void UpdateAndCheckBricks()
    {
        bricksLeft--;
        if (bricksLeft == 0)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
    }

    private void InstantiatePrefabs()
    {
        paddle = Instantiate(paddlePrefab);
        Ball ball = Instantiate(ballPrefab).GetComponent<Ball>();
        ball.paddle = paddle.transform;
        ball.gameManager = this;
    }
}
