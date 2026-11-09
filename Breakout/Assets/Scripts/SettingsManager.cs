using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> ballSprites = new List<Sprite>(9);

    [SerializeField]
    private List<Sprite> paddleSprites = new List<Sprite>(9);

    [SerializeField]
    private List<string> levels = new List<string>() { "Fácil", "Normal", "Difícil" };

    [SerializeField]
    private SpriteRenderer ballSprite;

    [SerializeField]
    private SpriteRenderer paddleSprite;

    [SerializeField]
    private TextMeshProUGUI levelText;

    private int ballSpriteIndex = 0;
    private int paddleSpriteIndex = 0;
    private int level = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballSprite.sprite = UserDataManager.Instance.BallSprite;
        paddleSprite.sprite = UserDataManager.Instance.PaddleSprite;

        level = UserDataManager.Instance.Level;
        levelText.text = levels[level];

        ballSpriteIndex = ballSprites.IndexOf(ballSprite.sprite);
        paddleSpriteIndex = paddleSprites.IndexOf(paddleSprite.sprite);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[Key.I].wasPressedThisFrame)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GoBallRight()
    {
        ballSpriteIndex++;
        if (ballSpriteIndex >= ballSprites.Count)
        {
            ballSpriteIndex = 0;
        }
        ChangeBallSprite();
    }

    public void GoBallLeft()
    {
        ballSpriteIndex--;
        if (ballSpriteIndex < 0)
        {
            ballSpriteIndex = ballSprites.Count - 1;
        }
        ChangeBallSprite();
    }

    private void ChangeBallSprite()
    {
        ballSprite.sprite = ballSprites[ballSpriteIndex];
        UserDataManager.Instance.ChangeBallSprite(ballSprites[ballSpriteIndex]);
    }

    public void GoPaddleRight()
    {
        paddleSpriteIndex++;
        if (paddleSpriteIndex >= paddleSprites.Count)
        {
            paddleSpriteIndex = 0;
        }
        ChangePaddleSprite();
    }

    public void GoPaddleLeft()
    {
        paddleSpriteIndex--;
        if (paddleSpriteIndex < 0)
        {
            paddleSpriteIndex = paddleSprites.Count - 1;
        }
        ChangePaddleSprite();
    }

    private void ChangePaddleSprite()
    {
        paddleSprite.sprite = paddleSprites[paddleSpriteIndex];
        UserDataManager.Instance.ChangePaddleSprite(paddleSprites[paddleSpriteIndex]);
    }

    public void GoLevelRight()
    {
        level++;
        if (level >= levels.Count)
        {
            level = 0;
        }
        ChangeLevel();
    }

    public void GoLevelLeft()
    {
        level--;
        if (level < 0)
        {
            level = levels.Count - 1;
        }
        ChangeLevel();
    }

    private void ChangeLevel()
    {
        levelText.text = levels[level];
        UserDataManager.Instance.ChangeLevel(level);
    }
}
