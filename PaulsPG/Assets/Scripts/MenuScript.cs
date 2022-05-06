using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
   
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu"); ;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
