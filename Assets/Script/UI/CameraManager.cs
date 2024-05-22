using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{


    private void Update()
    {
        transform.position = GameManager.Instance.CamPlayer.position;
        transform.rotation = GameManager.Instance.CamPlayer.rotation;
    }
}
