using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiTextDialogueSpeaker : MonoBehaviour
{
    public Image UiBack;
    public TextMeshProUGUI UiText;
    public Coroutine Coroutine;

    [SerializeField] AudioSource _source;
    [SerializeField] float _timerValueWait;
    [SerializeField] float _timerValueBetweenText;

    [SerializeField] List<ScriptableDialogue> _dialogue;
    [SerializeField] ScriptableDialogue _lastDialogue;
    float _timer;

    private void Update()
    {
        if (Coroutine == null)
            _timer += Time.deltaTime;

        if (_timer >= _timerValueWait && _lastDialogue != null)
        {
            StartDialogue(_lastDialogue);
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

    public void StartLastDialogue()
    {
        _timer = 0;

        if (Coroutine != null)
            StopCoroutine(Coroutine);

        ActiveUi(true);
        Coroutine = StartCoroutine(LaunchDialogue(_lastDialogue, _source, UiText, 0));
    }

    public void ActiveUi(bool var)
    {
        UiText.gameObject.SetActive(var);
        UiBack.gameObject.SetActive(var);
    }

    IEnumerator LaunchDialogue(ScriptableDialogue dialogue, AudioSource source, TextMeshProUGUI text, int i)
    {
        if (_dialogue.Count > 0)
        {
            dialogue.PlayDialogue(source, text, i);
            _lastDialogue = _dialogue[0];

            yield return new WaitForSeconds(dialogue.Voice[i].length + _timerValueBetweenText);

            i++;

            if (i < dialogue.Voice.Length || i < dialogue.Text.Length)
            {
                StartCoroutine(LaunchDialogue(dialogue, source, text, i));
            }
            else if (_dialogue.Count == 1)
            {
                _dialogue.RemoveAt(0);

                ActiveUi(false);
                Coroutine = null;
                i = 0;
            }
            else if (_dialogue.Count > 1)
            {
                _dialogue.RemoveAt(0);
                i = 0;

                Coroutine = StartCoroutine(LaunchDialogue(_dialogue[0], source, text, i)); ;
            }
        }
    }
}
