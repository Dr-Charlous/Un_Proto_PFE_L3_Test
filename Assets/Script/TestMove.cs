using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    [SerializeField] Transform _pos1;
    [SerializeField] Transform _pos2;
    [Range(0, 5)][SerializeField] float _value;

    private void Update()
    {
        Move(_value, _pos1.position, _pos2.position);
    }

    void Move(float value, Vector3 pos1, Vector3 pos2)
    {

        transform.position = pos1 + new Vector3(pos1.x + value, pos1.y + (value * value), pos1.z);
    }
}
