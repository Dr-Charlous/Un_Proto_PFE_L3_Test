using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[RequireComponent(typeof(CharaMove))]
[RequireComponent(typeof(Inventory))]
public class UI : MonoBehaviour
{
    [Header("UI :")]
    public GameObject UIObject;
    public GameObject[] FishUI;
    public GameObject[] LifeUI;
    public TextMeshProUGUI[] TextUI;
    private CharaMove _chara;
    private Inventory _inv;

    private void Start()
    {
        _chara = GetComponent<CharaMove>();
        _inv = GetComponent<Inventory>();

        UIObject.SetActive(false);
    }

    #region UI
    public void ShowUI()
    {
        UIObject.SetActive(true);

        for (int i = 0; i < FishUI.Length; i++)
        {
            if (i < _chara.Fish)
            {
                FishUI[i].SetActive(true);
            }
            else
            {
                FishUI[i].SetActive(false);
            }
        }

        for (int i = 0; i < LifeUI.Length; i++)
        {
            if (i < _chara.Life)
            {
                LifeUI[i].SetActive(true);
            }
            else
            {
                LifeUI[i].SetActive(false);
            }
        }

        for (int i = 0; i < _inv.ressourcesData.Count; i++)
        {
            TextUI[i].text = @$"{_inv.ressourcesData[i]} : {_inv.RessourcesDictionary[_inv.ressourcesData[i]]}";
        }
    }

    public void HideUI()
    {
        for (int i = 0; i < FishUI.Length; i++)
        {
            FishUI[i].SetActive(false);
        }

        for (int i = 0; i < FishUI.Length; i++)
        {
            LifeUI[i].SetActive(false);
        }

        UIObject.SetActive(false);
    }
    #endregion
}
