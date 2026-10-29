using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField]
    private int lives = 4;

    [SerializeField]
    private List<Sprite> sprites;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
                Destroy(gameObject);
            }
        }
    }

    private void UpdateSprite()
    {
        spriteRenderer.sprite = sprites[lives - 1];
    }
}
