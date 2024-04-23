using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class Jaws : MonoBehaviour
{
    [SerializeField] EnnemyStateController _ennemyStateController;
    [SerializeField] float _time;
    [SerializeField] Death _death;
    public bool IsBitting = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject.GetComponent<StateBabyController>() != null || other.GetComponent<CharaMove>() != null)
        {
            IsBitting = true;
            _death.ActiveUI();
        }
    }
}
