using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    [SerializeField]
    private Vector2 offset = new Vector2(3, 0);

    [SerializeField]
    private float speed = 2f;

    private Rigidbody2D rb;

    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 targetPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + offset;
        targetPosition = endPosition;
    }

    void FixedUpdate()
    {
        rb.MovePosition(
            Vector2.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime)
        );
        if (transform.position.Equals(targetPosition))
        {
            if (targetPosition.Equals(startPosition))
            {
                targetPosition = endPosition;
            }
            else
            {
                targetPosition = startPosition;
            }
        }
    }
}
