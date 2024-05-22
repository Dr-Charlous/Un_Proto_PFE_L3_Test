using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiTextDialogueSpeaker : MonoBehaviour
{
    public Image UiBack;
    public TextMeshProUGUI UiText;
    public Coroutine Coroutine;

    [SerializeField] AudioSource _source;
    [SerializeField] float _timerValueWait;

    float _timer;
    [SerializeField] List<ScriptableDialogue> _dialogue;

    private void Update()
    {
        if (Coroutine == null)
            _timer += Time.deltaTime;

        if (_timer >= _timerValueWait && _dialogue.Count > 0)
        {
            StartDialogue(_dialogue[0]);
        }
    }

    public void StartDialogue(ScriptableDialogue dialogue)
    {
        _dialogue.Add(dialogue);
        _timer = 0;

        if (Coroutine == null)
        {
            ActiveUi(true);
            Coroutine = StartCoroutine(LaunchDialogue(_dialogue[0], _source, UiText, 0));
        }
    }

    void ActiveUi(bool var)
    {
        UiText.gameObject.SetActive(var);
        UiBack.gameObject.SetActive(var);
    }

    IEnumerator LaunchDialogue(ScriptableDialogue dialogue, AudioSource source, TextMeshProUGUI text, int i)
    {
        dialogue.PlayDialogue(source, text, i);

        yield return new WaitForSeconds(dialogue.Voice[i].length + 0.1f);

        i++;

        if (i < dialogue.Voice.Length || i < dialogue.Text.Length)
        {
            StartCoroutine(LaunchDialogue(dialogue, source, text, i));
        }
        else
        {
            Coroutine = null;
            i = 0;

            if (_dialogue.Count > 1)
            {
                _dialogue.RemoveAt(0);
                Coroutine = StartCoroutine(LaunchDialogue(_dialogue[0], _source, UiText, 0));
            }
            else
                ActiveUi(false);
        }
    }
}
