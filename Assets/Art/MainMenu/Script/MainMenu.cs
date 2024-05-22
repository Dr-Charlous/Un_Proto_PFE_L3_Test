using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneAsset PlayScene;
    public SceneAsset CreditScene;
    public SceneAsset OptionScene;
    public SceneAsset MenuScene;

    public void PlayGame()
    {
        SceneManager.LoadScene(PlayScene.name);
    }

    public void Credits()
    {
        SceneManager.LoadScene(CreditScene.name);
    }

    public void Options()
    {
        SceneManager.LoadScene(OptionScene.name);
    }
    public void Back()
    {
        SceneManager.LoadScene(MenuScene.name);
    }
   
    public void QuitGame()
    {
        Application.Quit();
    }
}
