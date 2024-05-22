using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiTextDialogueOnGround : MonoBehaviour
{
    public ScriptableDialogue dialogue;

    private void OnTriggerEnter(Collider other)
    {
        CharaMove chara = other.GetComponent<CharaMove>();

        if (chara != null)
        {
            GameManager.Instance.Speaker.StartDialogue(dialogue);
            Destroy(this.gameObject);
        }
    }
}
