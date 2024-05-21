using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiTextDialogueOnGround : MonoBehaviour
{
    public ScriptableDialogue dialogue;
    [SerializeField] AudioSource Source;
    Coroutine _coroutine;
    TextDialogueManager _textManager;

    private void OnTriggerEnter(Collider other)
    {
        _textManager = other.GetComponentInChildren<TextDialogueManager>();

        if (other.GetComponent<CharaMove>() != null && _textManager != null && _coroutine == null)
        {
            _textManager.UiText.gameObject.SetActive(true);
            _textManager.UiBack.gameObject.SetActive(true);

            _coroutine = StartCoroutine(LaunchDialogue(Source, _textManager.UiText, 0));
        }
    }

    IEnumerator LaunchDialogue(AudioSource source, TextMeshProUGUI text, int i)
    {
        dialogue.PlayDialogue(source, text, i);

        yield return new WaitForSeconds(dialogue.Voice[i].length + 0.1f);

        i++;

        if (i < dialogue.Voice.Length || i < dialogue.Text.Length)
        {
            StartCoroutine(LaunchDialogue(source, text, i));
        }
        else
        {
            _coroutine = null;
            i = 0;

            if (_textManager != null)
            {
                _textManager.UiText.gameObject.SetActive(false);
                _textManager.UiBack.gameObject.SetActive(false);
                _textManager = null;
            }
        }
    }
}
