using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UiFollowing : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textMeshProGamePad;
    [SerializeField] TextMeshProUGUI _textMeshProKeyboard;
    [SerializeField] GameObject _objUiGamePad;
    [SerializeField] GameObject _objUiKeyboard;

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

        if (_objUiGamePad.activeInHierarchy || _objUiKeyboard.activeInHierarchy)
            CheckUiGamepad();
    }

    public void UpdateText(string text)
    {
        if (_textMeshProGamePad != null)
            _textMeshProGamePad.text = text;
        if (_textMeshProKeyboard != null)
            _textMeshProKeyboard.text = text;
    }

    public void ShowUi(bool value)
    {
        _objUiGamePad.SetActive(value);
        _objUiKeyboard.SetActive(value);

        if (value)
            CheckUiGamepad();
    }

    void CheckUiGamepad()
    {
        if (GameManager.Instance.Inputs.IsGamepad)
        {
            _objUiGamePad.SetActive(true);
            _objUiKeyboard.SetActive(false);
        }
        else
        {
            _objUiGamePad.SetActive(false);
            _objUiKeyboard.SetActive(true);
        }
    }
}
