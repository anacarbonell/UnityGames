using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject pipesPairPrefab;

    private float nextSpawnTime;
    private float timer = 0f;
    private bool isRunning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetRandomSpawnTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer >= nextSpawnTime)
        {
            timer = 0f;
            SetRandomSpawnTime();

            Transform pipesPair = Instantiate(pipesPairPrefab, transform).transform;

            Vector2 randomPosition = pipesPair.position;
            randomPosition.y += Random.Range(-1.5f, 1.5f);
            pipesPair.position = randomPosition;

            foreach (Transform pipe in pipesPair)
            {
                Vector2 pipePosition = pipe.position;
                pipePosition.y += Random.Range(-0.5f, 0.5f);
                pipe.position = pipePosition;
            }
        }
    }

    private void SetRandomSpawnTime()
    {
        nextSpawnTime = Random.Range(1.2f, 1.8f);
    }

    public void StartSpawning()
    {
        isRunning = true;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void StopSpawning()
    {
        isRunning = false;
    }
}
