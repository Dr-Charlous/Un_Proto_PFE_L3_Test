using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor.Overlays;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CharaMove : MonoBehaviour
{
    [Header("Values :")]
    public float moveSpeed = 5f;
    public float rotateSpeed = 0.5f;
    public float gravityScaleGround = 1f;
    public float gravityScaleWater = 0f;
    
    [Header("Water :")]
    public bool IsInWater = false;
    public Transform water = null;
    public LayerMask layerMaskGround;
    public LayerMask layerMaskWater;

    [Header("Child Components :")]
    public Transform Body;
    public Rigidbody _rb;
    public Gravity gravity;



    public void Start()
    {
        if (GetComponent<Gravity>())
        {
            Body = GetComponent<Transform>();
            _rb = GetComponent<Rigidbody>();
            gravity = GetComponent<Gravity>();
        }
    }

    void Update()
    {
        Rotate(KeyCode.LeftArrow, Vector3.down, rotateSpeed);
        Rotate(KeyCode.RightArrow, Vector3.up, rotateSpeed);

        if (IsInWater)
        {
            //Move(KeyCode.Space, Vector3.up, moveSpeed);
            //Move(KeyCode.LeftControl, Vector3.down, moveSpeed);

            gravity.gravityScale = gravityScaleWater;
        }
        else
        {
            gravity.gravityScale = gravityScaleGround;
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        Vector3 pos = transform.position;
        Physics.Raycast(new Vector3(pos.x, pos.y + 0.1f, pos.z), transform.TransformDirection(Vector3.back), out hit, 1);
        Debug.DrawRay(new Vector3(pos.x, pos.y+0.1f, pos.z), transform.TransformDirection(Vector3.back) * hit.distance, Color.yellow);


        if (IsInWater == true && water != null && Body.transform.position.y <= water.position.y)
        {
            MoveWater(KeyCode.UpArrow, Vector3.back, moveSpeed, water);
            MoveWater(KeyCode.DownArrow, Vector3.forward, moveSpeed, water);
        }
        else
        {
            Move(KeyCode.UpArrow, Vector3.back, moveSpeed);
            Move(KeyCode.DownArrow, Vector3.forward, moveSpeed);
        }

    }

    #region functionsMoveNRotation
    public void Move(KeyCode key, Vector3 transformMove, float moveSpeed)
    {
        if (Input.GetKey(key))
        {
            transform.Translate(transformMove * moveSpeed * Time.deltaTime);
        }
    }

    public void Rotate(KeyCode key, Vector3 transformMove, float rotateSpeed)
    {
        if (Input.GetKey(key))
        {
            transform.Rotate(transformMove * rotateSpeed * Time.deltaTime);
        }
    }

    public void MoveWater(KeyCode key, Vector3 transformMove, float moveSpeed, Transform water)
    {
        //positionLock
        var pos = transform.position;
        pos.y = water.position.y - 0.1f;
        transform.position = pos;

        //rotationLock
        var rot = transform.eulerAngles;
        rot.x = 0;
        rot.z = 0;
        transform.eulerAngles = rot;

        Move(key, transformMove, moveSpeed);
    }
    #endregion
}
