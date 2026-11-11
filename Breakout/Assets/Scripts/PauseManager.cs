using DesignPatterns.Singleton;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : PersistentSingleton<PauseManager>
{
    public bool GameIsPaused
    {
        get { return gameIsPaused; }
    }

    private bool gameIsPaused = false;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[Key.P].wasPressedThisFrame)
        {
            if (gameIsPaused)
            {
                gameIsPaused = false;
                Time.timeScale = 1f;
            }
            else
            {
                gameIsPaused = true;
                Time.timeScale = 0f;
            }
        }
    }
}
