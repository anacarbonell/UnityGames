using UnityEngine;

public class PipesPair : MonoBehaviour
{
    private GameManager gameManager;
    private AudioSource scoreSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scoreSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() { }

    void OnTriggerExit2D(Collider2D collision)
    {
        scoreSound.Play();
        gameManager.AddScore();
    }
}
