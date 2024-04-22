using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject UIDeath;

    private void Start()
    {
        UIDeath.SetActive(false);
    }
          
    public void ActiveUI()
    {
        UIDeath.SetActive(true);
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(gameObject.scene.name);
    }
}
