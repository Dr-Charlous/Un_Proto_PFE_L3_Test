using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Cinematic : MonoBehaviour
{
    [SerializeField] CharaMove _player;
    [SerializeField] Camera _camPrincipal;
    [SerializeField] Camera _camCine;

    [SerializeField] ScriptableDialogue _dialogueNidBuild;
    UiTextDialogueSpeaker _speaker;

    private void Start()
    {
        _camPrincipal = Camera.main;
        _camCine.enabled = false;
        _speaker = _player.GetComponentInChildren<UiTextDialogueSpeaker>();
    }

    public IEnumerator Cinematic1()
    {
        _player.IsParalysed = true;

        _camPrincipal.enabled = false;
        _camCine.enabled = true;

        yield return new WaitForSeconds(5);

        _camPrincipal.enabled = true;
        _camCine.enabled = false;

        _player.IsParalysed = false;

        if (_speaker != null && _dialogueNidBuild != null)
            _speaker.StartDialogue(_dialogueNidBuild);
    }
}
