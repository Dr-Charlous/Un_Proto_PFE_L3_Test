using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    #region singleton
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public CamController Character;
    public InputManager Inputs;
    public BabyManager BabyManager;
    public NestCreation Nest;
    public UiTextDialogueSpeaker Speaker;
    public CameraManager CamManager;
    public Transform CamPlayer;
    public EndPousuite EndPoursuite;
    public Death Death;
    public Death Win;
    public Respawn Respawn;
    public Animator Begin;
    public PauseMenu PauseMenu; 

    public float ReduceFloatValue(float value, int valueBelow)
    {
        return Mathf.Round(value * (10f * valueBelow)) / (10f * valueBelow);
    }
}
