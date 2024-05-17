using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMove : MonoBehaviour
{
    public float speed;
    public GameObject impactPrefab;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (speed != 0 && rb != null )
        {
            rb.position += transform.forward * (speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        speed = 0;

        ContactPoint contact = collision.contacts[0];   
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if (impactPrefab != null )
        {
            var impactVFX = Instantiate(impactPrefab, pos, rot) as GameObject;
            Destroy( impactVFX ,5 );
        }
        Destroy(gameObject);
    }
}
