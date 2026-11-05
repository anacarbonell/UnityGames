using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        Ready,
        Playing,
        GameOver,
    }

    private GameState state = GameState.Ready;

    public Ball ball;
    public Paddle player1Paddle;
    public Paddle player2Paddle;
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    public TextMeshProUGUI winnerText;
    public AudioSource goalSound;
    public TextMeshProUGUI launchText;

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
            case GameState.Ready:
                UpdateReady();
                break;
            case GameState.Playing:
                UpdatePlaying();
                break;
            case GameState.GameOver:
                UpdateGameOver();
                break;
            default:
                break;
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
        ResetState();
    }

    void UpdatePlaying() { }

    void UpdateGameOver()
    {
        ResetState();
    }

    private void ResetState()
    {
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            player1Goals = 0;
            player2Goals = 0;
            player1ScoreText.text = player1Goals.ToString();
            player2ScoreText.text = player2Goals.ToString();

            winnerText.gameObject.SetActive(false);
            launchText.gameObject.SetActive(true);

            player1Paddle.ResetSize();
            player2Paddle.ResetSize();

            state = GameState.Ready;
        }
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
