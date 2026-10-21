using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isPlayer1Wall;
    public GameManager gameManager;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (isPlayer1Wall)
            {
                gameManager.Player2Scores();
            }
            else
            {
                gameManager.Player1Scores();
            }
        }
    }
}
