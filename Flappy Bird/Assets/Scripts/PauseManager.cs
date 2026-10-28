using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseCanvas;

    public static bool gameIsPaused = false;
    private bool canPause = false;
    private AudioSource pauseSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canPause)
        {
            return;
        }
        if (Keyboard.current[Key.P].wasPressedThisFrame)
        {
            if (gameIsPaused)
            {
                gameIsPaused = false;
                pauseCanvas.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                gameIsPaused = true;
                pauseCanvas.SetActive(true);
                Time.timeScale = 0f;
            }
            pauseSound.Play();
        }
    }

    public void SetPauseActive(bool canPause)
    {
        this.canPause = canPause;
    }
}
