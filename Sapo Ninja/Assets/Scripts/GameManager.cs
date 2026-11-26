using DesignPatterns.Singleton;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private TextMeshProUGUI fruitText;

    private int fruit = 0;

    public void AddFruit()
    {
        fruit++;
        fruitText.text = fruit.ToString();
    }
}
