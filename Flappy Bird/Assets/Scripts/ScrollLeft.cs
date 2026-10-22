using UnityEngine;

public class ScrollLeft : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;

    private float startX;
    private float halfWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startX = transform.position.x;
        halfWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x <= startX - halfWidth)
        {
            transform.Translate(Vector2.right * halfWidth);
        }
    }
}
