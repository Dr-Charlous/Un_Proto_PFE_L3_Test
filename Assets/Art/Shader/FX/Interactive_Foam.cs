using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.VFX;

[ExecuteInEditMode]

public class Interactive_Foam : MonoBehaviour
{
    public Transform playerPos;
    public VisualEffect visualEffect;

    // Update is called once per frame
    void Update()
    {
        visualEffect.SetVector3("playerPos" , playerPos.position);   
    }
}
