using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    public bool isPlayer1;
    public float speed;
    public Rigidbody2D rb;
    public float minSize;
    public float mazSize;

    private Vector3 startPosition;
    private Vector3 startSize;

    protected virtual void Awake()
    {
        startPosition = transform.position;
        startSize = transform.localScale;
    }

    public void ResetPosition()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = startPosition;
    }

    public void ChangeSize(float amount)
    {
        Vector3 scale = transform.localScale;
        scale.y = Mathf.Clamp(scale.y + amount, minSize, mazSize);
        transform.localScale = scale;
    }

    public void ResetSize()
    {
        transform.localScale = startSize;
    }
}
