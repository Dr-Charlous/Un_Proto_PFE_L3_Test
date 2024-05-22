using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiTextDialogueOnGround : MonoBehaviour
{
    public ScriptableDialogue dialogue;
    UiTextDialogueSpeaker _speaker;

    private void Start()
    {
        _speaker = GameManager.Instance.Character.GetComponentInChildren<UiTextDialogueSpeaker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        CharaMove chara = other.GetComponent<CharaMove>();

        if (chara != null)
        {
            _speaker.StartDialogue(dialogue);
            Destroy(this.gameObject);
        }
    }
}
