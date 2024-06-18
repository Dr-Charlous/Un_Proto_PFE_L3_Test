using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] string _sceneMenu;

    public bool IsPaused;

    private void Start()
    {
        IsPaused = false;
    }

    public void ActivePause()
    {
        Time.timeScale = 0;
        GameManager.Instance.Inputs.IsPause = true;

        IsPaused = true;

        _pauseMenu.SetActive(true);
    }

    public void DesactivePause()
    {
        Time.timeScale = 1;
        GameManager.Instance.Inputs.IsPause = false;

        IsPaused = false;

        _pauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_sceneMenu);
    }
}
