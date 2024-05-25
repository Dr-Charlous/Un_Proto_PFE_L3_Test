using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] SceneAsset PlayScene;
    //[SerializeField] SceneAsset CreditScene;
    //[SerializeField] SceneAsset OptionScene;
    //[SerializeField] SceneAsset MenuScene;

    public void PlayGame()
    {
        SceneManager.LoadScene("PROG_Scene_04");
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
        SceneManager.LoadScene("Scene_Test_Art_3D_UI");
    }
   
    public void QuitGame()
    {
        Application.Quit();
    }
}
