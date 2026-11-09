using DesignPatterns.Singleton;
using UnityEngine;

public class UserDataManager : PersistentSingleton<UserDataManager>
{
    [SerializeField]
    private int startLives = 3;

    [SerializeField]
    private Sprite ballSprite;

    [SerializeField]
    private Sprite paddleSprite;

    public int Lives
    {
        get { return lives; }
    }

    public int MaxLives
    {
        get { return startLives; }
    }
    public int Score
    {
        get { return score; }
    }
    public Sprite BallSprite
    {
        get { return ballSprite; }
    }
    public Sprite PaddleSprite
    {
        get { return paddleSprite; }
    }
    public int Level
    {
        get { return level; }
    }

    private int lives;
    private int score;
    private int level = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetData();
    }

    // Update is called once per frame
    void Update() { }

    public void ResetData()
    {
        lives = startLives;
        score = 0;
    }

    public void LoseLive()
    {
        lives--;
    }

    public void AddScore()
    {
        score++;
    }

    public void ChangeBallSprite(Sprite ballSprite)
    {
        this.ballSprite = ballSprite;
    }

    public void ChangePaddleSprite(Sprite paddleSprite)
    {
        this.paddleSprite = paddleSprite;
    }

    public void ChangeLevel(int level)
    {
        this.level = level;
    }
}
