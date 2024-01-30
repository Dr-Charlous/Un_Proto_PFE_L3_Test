using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishNavigator : MonoBehaviour
{
    [SerializeField] Vector3 Direction;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharaMove>() != null || other.GetComponent<Gravity>() != null)
        {
            Direction = other.gameObject.transform.position - transform.position;
            Direction = new Vector3(Direction.x, 0, Direction.z);

            transform.Translate(-Direction * Time.deltaTime);
        }
    }
}
