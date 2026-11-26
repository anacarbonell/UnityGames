using UnityEngine;

public class PickUpBehaviour : MonoBehaviour
{
    [SerializeField]
    private float destroyTime = 0.85f;

    static readonly int PickUpParam = Animator.StringToHash("PickUp");

    private Animator animator;
    private Collider2D collider2d;
    private float destroyTimer = 0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        collider2d = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (destroyTimer > 0)
        {
            destroyTimer -= Time.deltaTime;
            if (destroyTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collider2d.enabled = false;
            animator.SetTrigger(PickUpParam);
            destroyTimer = destroyTime;
        }
    }
}
