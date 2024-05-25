using UnityEngine;
using UnityEngine.TextCore.Text;

public class ObjectCollectManager : MonoBehaviour
{
    [SerializeField] Transform parentCharacter;
    [SerializeField] GameObject ObjectFish;
    [SerializeField] ScriptableDialogue _dialogueFish;
    
    Transform parentOrigin;
    GameObject objectGet;
    Collider objectCollectCollider;
    bool grab;
    bool soundPlayed;

    private void Start()
    {
        grab = false;
        soundPlayed = false;
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.GetComponent<ObjectCollect>() != null)
        {
            Grab(collider);
        }
    }

    void Grab(Collider collider)
    {
        ObjectCollect objectInMouth = collider.GetComponent<ObjectCollect>();
        Transform objectTransform = collider.transform;

        if (objectInMouth != null && objectInMouth.gameObject == ObjectFish)
        {
            objectGet = objectInMouth.gameObject;
            parentOrigin = objectTransform.parent;
            objectTransform.SetParent(parentCharacter);
            grab = true;

            Animator animator = GetComponentInChildren<Animator>();

            if (animator != null)
                animator.SetTrigger("GetObj");

            if (_dialogueFish != null && !soundPlayed)
            {
                GameManager.Instance.Speaker.StartDialogue(_dialogueFish);
                soundPlayed = true;
            }
        }
    }
}
