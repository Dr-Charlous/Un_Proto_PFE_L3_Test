using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject _UIDeath;
    [SerializeField] GameObject _button;

    private void Start()
    {
        _UIDeath.SetActive(false);
    }
          
    public void ActiveUI()
    {
        _UIDeath.SetActive(true);
        GameManager.Instance.Character.IsParalysed = true;
        GameManager.Instance.Speaker.ActiveUi(false);
        GameManager.Instance.Speaker.gameObject.SetActive(false);
        GameManager.Instance.EventSystem.firstSelectedGameObject = _button;
        GameManager.Instance.Character.IsParalysed = true;
    }

    public void RetryButton()
    {
        //SceneManager.LoadScene(gameObject.scene.name);
        GameManager.Instance.Respawn.RespawnEntities();

        _UIDeath.SetActive(false);
        GameManager.Instance.Character.IsParalysed = false;
        GameManager.Instance.Begin.SetTrigger("Begin");
        GameManager.Instance.Begin.SetBool("End", false);
        GameManager.Instance.Speaker.gameObject.SetActive(true);
        GameManager.Instance.Speaker.StartLastDialogue();
        GameManager.Instance.Character.IsParalysed = false;
    }
}
