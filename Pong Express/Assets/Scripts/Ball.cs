using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public float speedMod;
    public AudioSource hitSound;
    public AudioClip wallHitClip;
    public AudioClip paddleHitClip;

    private Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    public void Launch(float directionX)
    {
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        rb.linearVelocity = new Vector2(directionX * speed, y * speed);
    }

    public void ResetPosition()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = startPosition;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hitSound.clip = paddleHitClip;
            rb.linearVelocity *= speedMod;
        }
        else
        {
            hitSound.clip = wallHitClip;
        }

        hitSound.Play();
    }
}
