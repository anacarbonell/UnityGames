using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject birdPrefab;

    [SerializeField]
    private Spawner spawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(birdPrefab);
    }

    // Update is called once per frame
    void Update() { }

    public void EndGame()
    {
        spawner.Stop();
    }
}
