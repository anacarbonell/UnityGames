using UnityEngine;

public class TargetTracker : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private bool horizontalMovement = true;

    void Start()
    {
        transform.position = target.position;
    }

    void FixedUpdate()
    {
        float x;
        float y;
        if (horizontalMovement)
        {
            x = target.position.x > transform.position.x ? target.position.x : transform.position.x;
            y = target.position.y;
        }
        else
        {
            y = target.position.y > transform.position.y ? target.position.y : transform.position.y;
            x = target.position.x;
        }
        transform.position = new Vector2(x, y);
    }
}
