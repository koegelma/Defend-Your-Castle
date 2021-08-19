using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "MainLevel";
    public void Play()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void Options()
    {
        Debug.Log("Options");
    }

    public void Quit()
    {
        Debug.Log("Exciting...");
        Application.Quit();
    }
}
