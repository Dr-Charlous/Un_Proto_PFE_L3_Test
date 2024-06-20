using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string _playScene;
    [SerializeField] string _creditScene;
    [SerializeField] string _menuScene;

    public void PlayGame()
    {
        SceneManager.LoadScene(_playScene);
    }

    public void Credits()
    {
        SceneManager.LoadScene(_creditScene);
    }

    public void Back()
    {
        SceneManager.LoadScene(_menuScene);
    }
   
    public void QuitGame()
    {
        Application.Quit();
    }
}
