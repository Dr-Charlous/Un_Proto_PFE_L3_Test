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

    public IEnumerator ActiveUI(bool value, bool isEnd)
    {
        //_UIDeath.SetActive(true);
        GameManager.Instance.Speaker.ActiveUi(false);
        GameManager.Instance.Speaker.gameObject.SetActive(false);
        GameManager.Instance.Character.IsParalysed = true;

        GameManager.Instance.Begin.SetBool("End", true);

        yield return new WaitForSeconds(1.5f);

        if (value)
            GameManager.Instance.Respawn.RespawnEntities(isEnd);
        else
            SceneManager.LoadScene(_scene);

        yield return new WaitForSeconds(1.5f);

        if (value)
        {
            GameManager.Instance.Speaker.gameObject.SetActive(true);
            GameManager.Instance.Speaker.ActiveUi(true);
            GameManager.Instance.Character.IsParalysed = false;
            GameManager.Instance.Speaker.StartLastDialogue();
        }

        GameManager.Instance.Begin.SetBool("End", false);
    }
}
