using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject _UIDeath;
    [SerializeField] string _scene;

    private void Start()
    {
        _UIDeath.SetActive(false);
    }

    public IEnumerator ActiveUI(bool value)
    {
        //_UIDeath.SetActive(true);
        GameManager.Instance.Speaker.ActiveUi(false);
        GameManager.Instance.Speaker.gameObject.SetActive(false);
        GameManager.Instance.Character.IsParalysed = true;

        GameManager.Instance.Begin.SetBool("End", true);

        yield return new WaitForSeconds(3);

        GameManager.Instance.Begin.SetBool("End", false);

        if (value)
        {
            GameManager.Instance.Respawn.RespawnEntities();

            //_UIDeath.SetActive(false);
            GameManager.Instance.Speaker.gameObject.SetActive(true);
            GameManager.Instance.Speaker.StartLastDialogue();
            GameManager.Instance.Character.IsParalysed = false;
        }
        else
        {
            SceneManager.LoadScene(_scene);
        }
    }
}
