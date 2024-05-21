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
    public Camera Cam;
    Camera MainCam;
    Coroutine coroutine;

    private void Start()
    {
        MainCam = Camera.main;
    }

    private void Update()
    {
        if (BeginCollider.ObjectTouch != null)
        {
            CharaMove chara = BeginCollider.ObjectTouch.GetComponent<CharaMove>();

            if (chara != null && coroutine == null)
            {
                coroutine = StartCoroutine(StartCine());
            }
        }

        if (EndCollider.ObjectTouch != null)
        {
            CharaMove chara1 = EndCollider.ObjectTouch.GetComponent<CharaMove>();

            if (chara1 != null)
            {
                Anim.SetTrigger("End");
                EndCollider.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    IEnumerator StartCine()
    {
        Anim.SetTrigger("Begin");

        MainCam.enabled = false;
        Cam.enabled = true;

        BeginCollider.GetComponent<BoxCollider>().enabled = false;  

        yield return new WaitForSeconds(5);

        MainCam.enabled = true;
        Cam.enabled = false;
        //Death = Instantiate(DeathPrefab);
    }
}
