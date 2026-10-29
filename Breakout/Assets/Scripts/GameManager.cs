using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int lives = 3;

    [SerializeField]
    private GameObject heartPrefab;

    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private GameObject paddlePrefab;

    private List<Heart> hearts = new List<Heart>();
    private GameObject paddle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < lives; i++)
        {
            Vector2 position = heartPrefab.transform.position;
            position.x += 0.5f * i;
            GameObject heart = Instantiate(heartPrefab, position, Quaternion.identity);
            hearts.Add(heart.GetComponent<Heart>());
        }

        InstantiatePrefabs();
    }

    // Update is called once per frame
    void Update() { }

    public void LoseLive()
    {
        Destroy(paddle);
        lives--;
        for (int i = hearts.Count; i > lives; i--)
        {
            hearts[i - 1].UpdateSprite(true);
        }

        if (lives > 0)
        {
            InstantiatePrefabs();
        }
        else
        {
            Debug.Log("Game Over");
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
