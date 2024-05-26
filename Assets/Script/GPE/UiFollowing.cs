using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiFollowing : MonoBehaviour
{
    [SerializeField] float _distance;
    [SerializeField] TextMeshProUGUI _textMeshPro;

    Camera _cam;
    Image _render;
    Canvas _canva;

    private void Start()
    {
        _cam = Camera.main;
        _render = GetComponentInChildren<Image>();
        _canva = GetComponentInChildren<Canvas>();
        _canva.worldCamera = _cam;
    }

    private void Update()
    {
        if (Vector3.Distance(GameManager.Instance.Character.transform.position, transform.position) < _distance)
        {
            _render.enabled = true;
            transform.LookAt(_cam.transform.position);
        }
        else
        {
            _render.enabled = false;
        }
    }

    public void UpdateText(string text)
    {
        if (_textMeshPro != null)
            _textMeshPro.text = text;
    }
}
