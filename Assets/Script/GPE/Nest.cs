using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Materials
{
    public string name;
    public int value = 0;
}

public class Nest : MonoBehaviour
{
    public Materials[] MaterialsC;
    public bool Crafted = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharaMove>() != null && other.GetComponent<Inventory>() != null && Crafted == false)
        {
            if (other.GetComponent<CharaMove>().Collecting && CraftingNest(other.GetComponent<Inventory>()) != other.GetComponent<Inventory>().RessourcesDictionary)
            {
                other.GetComponent<Inventory>().RessourcesDictionary = CraftingNest(other.GetComponent<Inventory>());
            }
        }
    }

    Dictionary<string, int> CraftingNest(Inventory inv)
    {
        int result = 0;
        var initial = inv.RessourcesDictionary;

        for (int i = 0; i < MaterialsC.Length; i++)
        {
            if (MaterialsC[i].name == inv.ressourcesData[i] && MaterialsC[i].value <= inv.RessourcesDictionary[MaterialsC[i].name])
            {
                inv.RessourcesDictionary[MaterialsC[i].name] -= MaterialsC[i].value;
                result++;
            }
        }

        if (result == MaterialsC.Length)
        {
            Crafted = true;
            return inv.RessourcesDictionary;
        }
        else
        {
            return initial;
        }
    }
}
