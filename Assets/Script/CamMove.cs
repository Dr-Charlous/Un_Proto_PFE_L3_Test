using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public GameObject objectToRotate;
    public float distance = 5;
    public float speed = 5;

    [Range(0f, 360f)]
    public float currentAngle = 0;
    [Range(0, 90)]
    public int currentXAngle = 45;

    private void Update()
    {
        if (currentAngle >= 360)
            currentAngle = 0;
        currentAngle += 1 * speed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(currentXAngle, currentAngle, 0);
        Vector3 position = objectToRotate.transform.position - (rotation * Vector3.forward * distance);
        transform.position = position;
        transform.rotation = rotation;
    }
}
