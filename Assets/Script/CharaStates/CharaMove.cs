using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 0.5f;
    public bool IsInWater = false;
    public Transform Body;
    public Rigidbody _rb;
    public Gravity gravity;
    public LayerMask layerMask;

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up * -1 * rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * 1 * rotateSpeed * Time.deltaTime);
        }


        if (IsInWater)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                transform.Translate(Vector3.up * -moveSpeed * Time.deltaTime);
            }
        }
    }

    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(Body.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(Body.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            //Debug.Log(hit.collider);
        }

        if (IsInWater == true)
        {
            gravity.gravityScale = 0;
        }
        else
        {
            gravity.gravityScale = 1;
        }
    }
}
