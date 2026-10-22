using UnityEngine;

public class ScrollLeft : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private bool loop = true;

    [SerializeField]
    private float destroyX = -11f;

    private float startX;
    private float halfWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startX = transform.position.x;
        if (loop)
        {
            halfWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (loop && transform.position.x <= startX - halfWidth)
        {
            transform.Translate(Vector2.right * halfWidth);
        }

        if (!loop && transform.position.x <= destroyX)
        {
            Destroy(gameObject);
        }
    }
}
