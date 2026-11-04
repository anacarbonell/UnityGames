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

    [SerializeField]
    private AudioClip levelChangeClip;

    [SerializeField]
    private List<GameObject> powerUpPrefabs = new List<GameObject>(2);

    private UserDataManager userDataManager;
    private int currentScene;

    private List<Heart> hearts = new List<Heart>();
    private GameObject paddle;
    private int bricksLeft;
    private List<GameObject> powerUps = new List<GameObject>();
    private int ballsLeft = 0;

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
            GoNextScene();
        }

        for (int i = 0; i < powerUps.Count; i++)
        {
            if (powerUps[i] == null)
            {
                powerUps.RemoveAt(i);
            }
            else if (powerUps[i].transform.position.y < -5.5f)
            {
                Destroy(powerUps[i]);
                powerUps.RemoveAt(i);
            }
        }
    }

    public void DestroyBall()
    {
        ballsLeft--;
        if (ballsLeft == 0)
        {
            LoseLive();
        }
    }

    private void LoseLive()
    {
        Destroy(paddle);
        userDataManager.LoseLive();
        for (int i = hearts.Count; i > userDataManager.Lives; i--)
        {
            hearts[i - 1].UpdateSprite(true);
        }

        if (userDataManager.Lives > 0)
        {
            foreach (GameObject powerUp in powerUps)
            {
                Destroy(powerUp);
            }
            powerUps.Clear();
            InstantiatePrefabs();
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void ManageBreakingBrick(Vector2 position)
    {
        bricksLeft--;
        if (bricksLeft == 0)
        {
            GoNextScene();
        }
        SpawnPowerUp(position);
    }

    private void SpawnPowerUp(Vector2 position)
    {
        int randomPowerUp = Random.Range(0, 100);
        float powerUpPercent = 40f / powerUpPrefabs.Count;
        for (int i = 0; i < powerUpPrefabs.Count; i++)
        {
            if (randomPowerUp < powerUpPercent * (i + 1))
            {
                GameObject powerUp = Instantiate(powerUpPrefabs[i], position, Quaternion.identity);
                powerUps.Add(powerUp);
                break;
            }
        }
    }

    private void GoNextScene()
    {
        AudioManager.Instance.PlayClip(levelChangeClip);
        SceneManager.LoadScene(currentScene + 1);
    }

    private void InstantiatePrefabs()
    {
        paddle = Instantiate(paddlePrefab);
        paddle.GetComponent<Paddle>().gameManager = this;
        AddBall(false);
    }

    public void AddBall(bool launch)
    {
        Ball ball = Instantiate(ballPrefab).GetComponent<Ball>();
        ball.paddle = paddle.transform;
        ball.gameManager = this;
        ball.launchAtStart = launch;
        ballsLeft++;
    }
}
