using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject _UIDeath;
    [SerializeField] CharaMove _chara;

    private void Start()
    {
        _UIDeath.SetActive(false);
    }
          
    public void ActiveUI()
    {
        _UIDeath.SetActive(true);
        _chara.IsParalysed = true;
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(gameObject.scene.name);
    }
}
