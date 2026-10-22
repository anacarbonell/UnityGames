using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : MonoBehaviour
{
    [SerializeField]
    private float jumpSpeed = 5f;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (
            (
                Keyboard.current.spaceKey.wasPressedThisFrame
                || Mouse.current.leftButton.wasPressedThisFrame
            )
            && transform.position.y < 4
        )
        {
            rb.linearVelocityY = jumpSpeed;
        }
    }
}
