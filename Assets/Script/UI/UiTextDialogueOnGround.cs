using UnityEngine;

public class UiTextDialogueOnGround : MonoBehaviour
{
    public ScriptableDialogue dialogue;

    private void OnTriggerEnter(Collider other)
    {
        CamController chara = other.GetComponent<CamController>();

        if (chara != null)
        {
            GameManager.Instance.Speaker.StartDialogue(dialogue);
            Destroy(this.gameObject);
        }
    }
}
