using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameOver;
    public GameObject gameOverUI;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GameManager in scene!");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        isGameOver = false;
    }
    public void GameOver()
    {
        isGameOver = true;
        gameOverUI.SetActive(true);
    }
}
