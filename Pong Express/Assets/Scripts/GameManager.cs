using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        ModeSelection,
        Ready,
        Playing,
        GameOver,
        Paused,
    }

    private GameState state = GameState.ModeSelection;

    public Ball ball;
    public Paddle player1Paddle;
    private Paddle player2Paddle;
    public ManualPaddle player2Manual;
    public AutoPaddle player2Auto;
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    public TextMeshProUGUI winnerText;
    public AudioSource goalSound;
    public TextMeshProUGUI launchText;
    public Canvas modeSelectionCanvas;
    public Canvas pauseCanvas;

    public int pointsToWin;

    private int player1Goals = 0;
    private int player2Goals = 0;

    private float launchDirectionX;

    void Start()
    {
        launchDirectionX = Random.Range(0, 2) == 0 ? -1 : 1;
    }

    void Update()
    {
        switch (state)
        {
            case GameState.ModeSelection:
                UpdateModeSelection();
                break;
            case GameState.Ready:
                UpdateReady();
                break;
            case GameState.Playing:
                UpdatePlaying();
                break;
            case GameState.GameOver:
                UpdateGameOver();
                break;
            case GameState.Paused:
                UpdatePaused();
                break;
            default:
                break;
        }
    }

    void UpdateModeSelection()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SelectMode(true);
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            SelectMode(false);
        }
    }

    void UpdateReady()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            state = GameState.Playing;
            launchText.gameObject.SetActive(false);
            ball.Launch(launchDirectionX);
        }

        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            ResetState();
            state = GameState.Ready;
        }
    }

    void UpdatePlaying()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            Time.timeScale = 0f;
            pauseCanvas.gameObject.SetActive(true);
            state = GameState.Paused;
        }
    }

    void UpdateGameOver()
    {
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            ResetState();

            winnerText.gameObject.SetActive(false);
            modeSelectionCanvas.gameObject.SetActive(true);

            state = GameState.ModeSelection;
        }
    }

    void UpdatePaused()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            pauseCanvas.gameObject.SetActive(false);
            state = GameState.Playing;
        }
    }

    private void SelectMode(bool isSinglePlayer)
    {
        player2Auto.enabled = isSinglePlayer;
        player2Manual.enabled = !isSinglePlayer;
        player2Paddle = isSinglePlayer ? player2Auto : player2Manual;
        StartGame();
    }

    private void StartGame()
    {
        player1Paddle.enabled = true;
        modeSelectionCanvas.gameObject.SetActive(false);
        launchText.gameObject.SetActive(true);
        state = GameState.Ready;
    }

    private void ResetState()
    {
        player1Goals = 0;
        player2Goals = 0;
        player1ScoreText.text = player1Goals.ToString();
        player2ScoreText.text = player2Goals.ToString();

        player1Paddle.ResetSize();
        player2Paddle.ResetSize();
    }

    public void Player1Scores()
    {
        player1Goals++;
        player1ScoreText.text = player1Goals.ToString();
        goalSound.Play();

        launchDirectionX = -1;

        ResetPositions();
        ChangePaddleSizes(true);

        if (player1Goals == pointsToWin)
        {
            ShowWinner(true);
        }
        else
        {
            state = GameState.Ready;
        }
    }

    public void Player2Scores()
    {
        player2Goals++;
        player2ScoreText.text = player2Goals.ToString();
        goalSound.Play();

        launchDirectionX = 1;

        ResetPositions();
        ChangePaddleSizes(false);

        if (player2Goals == pointsToWin)
        {
            ShowWinner(false);
        }
        else
        {
            state = GameState.Ready;
        }
    }

    private void ChangePaddleSizes(bool player1Scored)
    {
        float player1Change = 0.5f * (player1Scored ? -1 : 1);
        player1Paddle.ChangeSize(player1Change);
        player2Paddle.ChangeSize(player1Change * -1);
    }

    public void ResetPositions()
    {
        ball.ResetPosition();
        player1Paddle.ResetPosition();
        player2Paddle.ResetPosition();
        launchText.gameObject.SetActive(true);
    }

    private void ShowWinner(bool isPlayer1)
    {
        state = GameState.GameOver;
        if (isPlayer1)
        {
            winnerText.text = "¡Gana el jugador 1!";
            winnerText.color = new Color32(178, 100, 250, 255);
        }
        else
        {
            winnerText.text = "¡Gana el jugador 2!";
            winnerText.color = new Color32(60, 213, 160, 255);
        }

        launchText.gameObject.SetActive(false);
        winnerText.gameObject.SetActive(true);
    }
}
