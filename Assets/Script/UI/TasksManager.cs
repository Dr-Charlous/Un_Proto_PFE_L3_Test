using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TasksManager : MonoBehaviour
{
    [SerializeField] List<Tasks> _tasks;
    [SerializeField] GameObject _tasksShowPrefab;
    [SerializeField] Transform _taskBoard;
    [SerializeField] CharaMove _chara;

    [HideInInspector] public List<GameObject> _tasksShow;

    public void ShowTasks()
    {
        HideTasks();

        for (int i = 0; i < _tasks.Count; i++)
        {
            GameObject obj = Instantiate(_tasksShowPrefab, Vector3.zero, Quaternion.identity, _taskBoard);
            _tasksShow.Add(obj);
        }

        for (int i = 0; i < _tasksShow.Count; i++)
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

    public void AddTask(Tasks task)
    {
        _chara.UI.SetActive(true);
        ShowTasks();

        _tasks.Add(task);
    }

    public void RemoveTask(Tasks task)
    {
        _tasks.Remove(task);
    }
}
