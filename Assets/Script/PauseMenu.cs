using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] string _sceneMenu;

    private void Update()
    {
        if (Time.timeScale == 0 && !GameManager.Instance.Inputs.IsGamepad)
            Cursor.visible = true;
    }

    public void ActivePause()
    {
        Time.timeScale = 0;
        GameManager.Instance.Inputs.IsPause = true;

        _pauseMenu.SetActive(true);
    }

    public void DesactivePause()
    {
        Time.timeScale = 1;
        GameManager.Instance.Inputs.IsPause = false;

        Cursor.visible = false;

        _pauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(_sceneMenu);
    }
}
