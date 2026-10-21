using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
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

    private bool waitingForLaunch = true;
    private bool isGameFinished;

    private float launchDirectionX;

    void Start()
    {
        launchDirectionX = Random.Range(0, 2) == 0 ? -1 : 1;
    }

    void Update()
    {
        if (waitingForLaunch)
        {
            if (!isGameFinished && Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                waitingForLaunch = false;
                launchText.gameObject.SetActive(false);
                ball.Launch(launchDirectionX);
            }
            else if (Keyboard.current.nKey.wasPressedThisFrame)
            {
                ResetState();
            }
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
        waitingForLaunch = true;
        launchText.gameObject.SetActive(true);
    }

    private void ShowWinner(bool isPlayer1)
    {
        isGameFinished = true;
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

    private void ResetState()
    {
        player1Goals = 0;
        player2Goals = 0;
        player1ScoreText.text = player1Goals.ToString();
        player2ScoreText.text = player2Goals.ToString();

        winnerText.gameObject.SetActive(false);
        launchText.gameObject.SetActive(true);

        player1Paddle.ResetSize();
        player2Paddle.ResetSize();

        isGameFinished = false;
    }
}
