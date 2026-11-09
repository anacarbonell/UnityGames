using DesignPatterns.Singleton;
using UnityEngine;

public class UserDataManager : PersistentSingleton<UserDataManager>
{
    [SerializeField]
    private int startLives = 3;

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

    private int lives;
    private int score;

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
}
