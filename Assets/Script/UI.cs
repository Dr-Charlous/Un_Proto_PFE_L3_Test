using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[RequireComponent(typeof(CharaMove))]
public class UI : MonoBehaviour
{
    [Header("UI :")]
    public GameObject UIObject;
    public GameObject[] FishUI;
    public GameObject[] LifeUI;
    private CharaMove chara;

    private void Start()
    {
        chara = GetComponent<CharaMove>();
        UIObject.SetActive(false);
    }

    #region UI
    public void ShowUI()
    {
        UIObject.SetActive(true);

        for (int i = 0; i < FishUI.Length; i++)
        {
            if (i < chara.Fish)
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
            if (i < chara.Life)
            {
                LifeUI[i].SetActive(true);
            }
            else
            {
                LifeUI[i].SetActive(false);
            }
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
