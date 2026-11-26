using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int actualScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(actualScene == 0 ? 1 : 0);
        }
    }
}
