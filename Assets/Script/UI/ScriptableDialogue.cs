using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueBabyReccup", menuName = "ScriptableObjects/DialogueBabyReccup", order = 1)]
public class ScriptableDialogue : ScriptableObject
{
    [TextArea]
    public string[] Text;
    public AudioClip[] Voice;

    public void PlayDialogue(AudioSource source, TextMeshProUGUI text, int i)
    {
        if (source.isPlaying)
            return;

        text.text = Text[i];
        source.clip = Voice[i];
        source.Play();
    }
}
