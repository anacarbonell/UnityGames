using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> sprites = new List<Sprite>(2);

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateSprite(bool isEmpty)
    {
        spriteRenderer.sprite = sprites[isEmpty ? 1 : 0];
    }
}
