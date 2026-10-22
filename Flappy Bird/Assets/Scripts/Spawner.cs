using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject pipesPairPrefab;

    private float nextSpawnTime;
    private float timer = 0f;
    private bool isRunning = true;

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

            Vector2 randomPosition = pipesPairPrefab.transform.position;
            randomPosition.y += Random.Range(-1.5f, 1.5f);
            GameObject pipesPair = Instantiate(
                pipesPairPrefab,
                randomPosition,
                Quaternion.identity
            );

            Transform topPipe = pipesPair.transform.Find("TopPipe");
            Vector2 topPipePosition = topPipe.position;
            topPipePosition.y += Random.Range(-0.5f, 0.5f);
            topPipe.position = topPipePosition;

            Transform bottomPipe = pipesPair.transform.Find("BottomPipe");
            Vector2 bottomPipePosition = bottomPipe.position;
            bottomPipePosition.y += Random.Range(-0.5f, 0.5f);
            bottomPipe.position = bottomPipePosition;
        }
    }

    private void SetRandomSpawnTime()
    {
        nextSpawnTime = Random.Range(1.2f, 1.8f);
    }

    public void Stop()
    {
        isRunning = false;
    }
}
