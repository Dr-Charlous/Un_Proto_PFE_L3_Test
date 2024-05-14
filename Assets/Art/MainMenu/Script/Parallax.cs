using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public float offsetMultiplier = 0.1f;
    public float smoothTime = .3f;

    private Vector2 startPosition;
    private Vector3 velocity;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        Vector2 offset = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 targetPos = Vector3.SmoothDamp(transform.position, startPosition + (offset * offsetMultiplier), ref velocity, smoothTime);
        transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
    }

    //Merci Antoine pour avoir corrigé le code !!! :3

}
