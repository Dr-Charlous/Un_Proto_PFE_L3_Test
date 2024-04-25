using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    [SerializeField] CharaMove _player;
    [SerializeField] Camera _camPrincipal;
    [SerializeField] Camera _camCine;

    private void Start()
    {
        _camPrincipal = Camera.main;
        _camCine.enabled = false;
    }

    public IEnumerator Cinematic1()
    {
        _player.IsParalysed = true;

        _camPrincipal.enabled = false;
        _camCine.enabled = true;

        yield return new WaitForSeconds(5);

        _camPrincipal.enabled = true;
        _camCine.enabled = false;

        _player.IsParalysed = false;
    }
}
