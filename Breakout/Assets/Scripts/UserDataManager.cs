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

    private int lives;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetLives();
    }

    // Update is called once per frame
    void Update() { }

    public void ResetLives()
    {
        lives = startLives;
    }

    public void LoseLive()
    {
        lives--;
    }
}
