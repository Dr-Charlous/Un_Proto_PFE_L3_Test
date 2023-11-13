using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FishingZone : MonoBehaviour
{
    public int Fishies = 3;
    public int rate = 1000;
    public bool IsTouching = false;
    public BoxCollider collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharaMove>() != null)
        {
            other.GetComponent<CharaMove>().Fish += Fishing(other.GetComponent<CharaMove>());
            IsTouching = true;
        }
        else
        {
            IsTouching = false;
        }
    }

    int Fishing(CharaMove chara)
    {
        int fish = 0;

        if (chara.Fishinning == true && chara.Fish < 5 && chara.Fishinning == true && Fishies > 0)
        {
            int randomNumber = UnityEngine.Random.Range(0, rate);

            if (randomNumber == 0)
            {
                Fishies--;
                fish = 1;
            }
            else
            {
                fish = 0;
            }
        }

        return fish;
    }
}
