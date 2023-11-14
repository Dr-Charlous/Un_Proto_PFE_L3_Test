using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ressources : MonoBehaviour
{
    public enum ressources
    {
        Stick,
        Rock,
        Plant
    }

    public ressources Item;
    public int Quantity;
    private new BoxCollider collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
    }
}
