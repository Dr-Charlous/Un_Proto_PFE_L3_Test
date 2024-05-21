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
            UiTextDialogueSpeaker speaker = chara.GetComponentInChildren<UiTextDialogueSpeaker>();

            if (speaker != null && speaker.Coroutine == null)
            {
                speaker.StartDialogue(dialogue);

                Destroy(this.gameObject);
            }
        }
    }
}
