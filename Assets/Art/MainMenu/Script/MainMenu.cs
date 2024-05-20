using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void Credits()
    {
        SceneManager.LoadScene("Scene_Test_Credits");
    }

    public void Options()
    {
        SceneManager.LoadScene("Scene_Test_Options");
    }
    public void Back()
    {
        SceneManager.LoadScene("Scene_Test_Art_UI");
    }
   
    public void QuitGame()
    {
        Application.Quit();
    }



}
