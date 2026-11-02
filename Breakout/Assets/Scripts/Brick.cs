using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField]
    private int lives = 4;

    [SerializeField]
    private List<Sprite> sprites = new List<Sprite>(4);

    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UpdateSprite();
    }

    // Update is called once per frame
    void Update() { }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            lives--;
            if (lives > 0)
            {
                UpdateSprite();
            }
            else
            {
                gameManager.UpdateAndCheckBricks();
                Destroy(gameObject);
            }
        }
    }

    private void UpdateSprite()
    {
        spriteRenderer.sprite = sprites[lives - 1];
    }
}
