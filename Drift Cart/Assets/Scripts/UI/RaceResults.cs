using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceResults : MonoBehaviour
{
    public GameManager gameManager;
    public List<TMP_Text> names = new List<TMP_Text>();
    private void Start()
    {
        names[gameManager.finishPosition - 1].text = "You";
    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }
}
