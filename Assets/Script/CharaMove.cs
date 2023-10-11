using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody _rb;

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _rb.AddForce(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            _rb.AddForce(Vector3.back * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rb.AddForce(Vector3.left * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _rb.AddForce(Vector3.right * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * moveSpeed * Time.deltaTime);
        }
    }
}
