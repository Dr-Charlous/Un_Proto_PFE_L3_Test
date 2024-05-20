using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class ScriptableDialogue : ScriptableObject
{
    public string[] Text;
    public AudioClip[] Voice;

    public void PlayDialogue(AudioSource source, TextMeshProUGUI text, int i)
    {
        if (source.isPlaying)
            return;

        source.clip = Voice[i];
        source.Play();
        text.text = Text[i];
    }
}
