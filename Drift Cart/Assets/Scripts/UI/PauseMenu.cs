using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject MainUI;
    public GameObject pauseMenu;

    public void ResumeGame()
    {
        MainUI.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        MainUI.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
