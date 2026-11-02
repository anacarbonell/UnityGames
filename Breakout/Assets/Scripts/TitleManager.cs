using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private bool isTitle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[Key.Space].wasPressedThisFrame)
        {
            if (isTitle)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
