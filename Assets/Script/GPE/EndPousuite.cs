using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPousuite : MonoBehaviour
{
    public OnTriggerEnterScript BeginCollider;
    public OnTriggerEnterScript EndCollider;
    public Animator Anim;
    public GameObject DeathPrefab;
    public GameObject Death;

    private void Update()
    {
        if (BeginCollider.ObjectTouch != null)
        {
            CharaMove chara = BeginCollider.ObjectTouch.GetComponent<CharaMove>();
        }

        if (EndCollider.ObjectTouch != null)
        {
            CharaMove chara1 = EndCollider.ObjectTouch.GetComponent<CharaMove>();
        }
    }
}
