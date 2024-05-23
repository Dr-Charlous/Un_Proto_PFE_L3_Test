using UnityEngine;

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

    public CharaMove Character;
    public BabyManager BabyManager;
    public NestCreation Nest;
    public UiTextDialogueSpeaker Speaker;
    public CameraManager CamManager;
    public Transform CamPlayer;
    public Death Death;
    public Death Win;
}
