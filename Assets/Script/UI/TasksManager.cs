using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TasksManager : MonoBehaviour
{
    [SerializeField] Tasks[] _tasks;
    [SerializeField] GameObject _tasksShowPrefab;
    [SerializeField] Transform _taskBoard;

    [HideInInspector] public List<GameObject> _tasksShow;

    public void ShowTasks(int begin, int end)
    {
        for (int i = 0; i <= end - begin; i++)
        {
            GameObject obj = Instantiate(_tasksShowPrefab, Vector3.zero, Quaternion.identity, _taskBoard);
            _tasksShow.Add(obj);
        }

        for (int i = begin; i <= end; i++)
        {
            _tasksShow[i].GetComponentInChildren<TextMeshProUGUI>().text = _tasks[i]._textTask;
        }
    }

    public void HideTasks()
    {
        for (int i = 0; i < _tasksShow.Count; i++)
        {
            Destroy(_tasksShow[i]);
        }

        _tasksShow.Clear();
    }
}
