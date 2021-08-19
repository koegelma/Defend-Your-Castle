using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    private void Update()
    {
        if (GameManager.instance.isGameOver) return;
        if (Input.GetKeyDown(KeyCode.Escape)) Toggle();
    }
    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf) Time.timeScale = 0f;
        else Time.timeScale = 1f;

    }

    public void Retry()
    {
        Toggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
