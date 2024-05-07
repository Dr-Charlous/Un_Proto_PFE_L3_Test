using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTextDialogueOnGround : MonoBehaviour
{
    public string Text;

    private void OnTriggerEnter(Collider other)
    {
        TextDialogueManager textManager = other.GetComponentInChildren<TextDialogueManager>();

        if (other.GetComponent<CharaMove>() != null && textManager != null)
        {
            textManager.UiText.text = Text;
            textManager.UiText.gameObject.SetActive(true);
            textManager.UiBack.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TextDialogueManager textManager = other.GetComponentInChildren<TextDialogueManager>();

        if (other.GetComponent<CharaMove>() != null && textManager != null)
        {
            textManager.UiText.gameObject.SetActive(false);
            textManager.UiBack.gameObject.SetActive(false);
        }
    }
}
