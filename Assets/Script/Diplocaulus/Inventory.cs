using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(CharaMove))]
public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> RessourcesDictionary = new Dictionary<string, int>();
    public bool InRessourcesSource = false;
    public bool CollectingR = false;
    public List<string> ressourcesData = new List<string>();
    [SerializeField] private Ressources _ressources;
    [SerializeField] private CharaMove _chara;

    private void Start()
    {
        _chara = GetComponent<CharaMove>();

        RessourcesDictionary.Add("Stick", 0);
        ressourcesData.Add("Stick");
        RessourcesDictionary.Add("Rock", 0);
        ressourcesData.Add("Rock");
        RessourcesDictionary.Add("Plant", 0);
        ressourcesData.Add("Plant");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Ressources>() != null)
        {
            _ressources = other.GetComponent<Ressources>();

            if (CollectingR == false)
            {
                StartCoroutine(Collect(_chara, _ressources));
            }

            InRessourcesSource = true;
        }
        else
        {
            InRessourcesSource = false;
        }
    }

    public IEnumerator Collect(CharaMove chara, Ressources ress)
    {
        if (chara.Collecting == true)
        {
            CollectingR = true;
            ress.Quantity--;

            RessourcesDictionary[ress.Item.ToString()]++;

            yield return new WaitForSeconds(1);
            CollectingR = false;

            if (ress.Quantity <= 0)
            {
                Destroy(ress.gameObject);
            }
        }
    }
}
