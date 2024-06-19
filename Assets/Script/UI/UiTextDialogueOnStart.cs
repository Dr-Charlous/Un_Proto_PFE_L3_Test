using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTextDialogueOnStart : MonoBehaviour
{
    public ScriptableDialogue dialogue;

    private void Start()
    {
        GameManager.Instance.Speaker.StartDialogue(dialogue);
    }
}
