using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameOver = false;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GameManager in scene!");
            return;
        }
        instance = this;
    }
    public void GameOver()
    {
        Debug.Log("Game Over!");
        isGameOver = true;
    }
}
