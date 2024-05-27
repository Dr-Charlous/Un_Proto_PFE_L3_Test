using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiFollowing : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textMeshPro;
    [SerializeField] GameObject _obj;

    Camera _cam;
    Canvas _canva;

    private void Start()
    {
        _cam = Camera.main;
        _canva = GetComponentInChildren<Canvas>();
        _canva.worldCamera = _cam;

        ShowUi(false);
    }

    private void Update()
    {
        transform.LookAt(_cam.transform.position);
    }

    public void UpdateText(string text)
    {
        if (_textMeshPro != null)
            _textMeshPro.text = text;
    }

    public void ShowUi(bool value)
    {
        _obj.SetActive(value);
    }
}
