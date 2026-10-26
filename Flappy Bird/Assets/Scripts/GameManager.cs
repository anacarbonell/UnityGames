using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject birdPrefab;

    [SerializeField]
    private Spawner spawner;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject medalPrefab;

    [SerializeField]
    private float medalRowSpacing = 0.9f;

    [SerializeField]
    private float medalColumnSpacing = 0.8f;

    [SerializeField]
    private int medalsPerColumn = 8;

    private int score = 0;
    private List<GameObject> medalList = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(birdPrefab);
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update() { }

    public void EndGame()
    {
        spawner.Stop();
    }

    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();

        if (score % 5 == 0)
        {
            int medalCount = medalList.Count;
            Vector2 medalPosition = medalPrefab.transform.position;

            if (medalCount % medalsPerColumn == 0)
            {
                medalPosition.x += medalCount / medalsPerColumn * medalColumnSpacing;
                medalCount = 0;
            }

            medalPosition.y -= medalCount * medalRowSpacing;
            GameObject medal = Instantiate(medalPrefab, medalPosition, Quaternion.identity);
            medalList.Add(medal);
        }
    }
}
