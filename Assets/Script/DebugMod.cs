using TMPro;
using UnityEngine;

public class DebugMod : MonoBehaviour
{
    [SerializeField] InputManager _inputManager;
    [SerializeField] CharaMove _charaMove;
    [SerializeField] StateEnnemyController[] _ennemyController;
    [SerializeField] int _ennemyNumber;
    [SerializeField] StateBabyController[] _babyController;
    [SerializeField] int _babyNumber;
    [SerializeField] ObjectCollectController _objCollectController;

    [SerializeField] TextMeshProUGUI[] _textMeshPro;
    [SerializeField] GameObject _canvaDebug;

    bool _debugModShow;

    private void Start()
    {
        _debugModShow = false;
        _ennemyNumber = 0;
        _babyNumber = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            _debugModShow = !_debugModShow;
            _canvaDebug.SetActive(_debugModShow);
        }

        if (Input.GetKeyDown(KeyCode.F2) && _ennemyNumber > 0)
        {
            _ennemyNumber--;
        }

        if (Input.GetKeyDown(KeyCode.F3) && _ennemyNumber < _ennemyController.Length - 1)
        {
            _ennemyNumber++;
        }

        if (Input.GetKeyDown(KeyCode.F4) && _babyNumber > 0)
        {
            _babyNumber--;
        }

        if (Input.GetKeyDown(KeyCode.F5) && _babyNumber < _babyController.Length - 1)
        {
            _babyNumber++;
        }

        if (_debugModShow)
        {
            UpdateText(PlayerData(), _textMeshPro[0]);

            UpdateText(EnnemyData(_ennemyController, _ennemyNumber), _textMeshPro[1]);

            UpdateText(BabyData(_babyController, _babyNumber), _textMeshPro[2]);

            Gpe();

            UpdateText(InputData(), _textMeshPro[4]);
        }
    }

    void UpdateText(string text, TextMeshProUGUI _textMeshPro)
    {
        _textMeshPro.text = text;
    }

    string PlayerData()
    {
        string dataPalyer = "";

        dataPalyer += $"---- Name : {_charaMove.transform.name} ---- \n";
        dataPalyer += $"\n";

        dataPalyer += $"Speed Move : {_charaMove.Rb.velocity} \n";
        dataPalyer += $"Speed Rotate : {_charaMove.Rb.angularVelocity} \n";
        dataPalyer += $"\n";

        dataPalyer += $"Acceleration : {_charaMove.Acceleration} \n";
        dataPalyer += $"Limit Max Speed : {_charaMove.LimitMaxSpeed} \n";
        dataPalyer += $"Decrease Speed : {_charaMove.DecreaseSpeed} \n";
        dataPalyer += $"Steering : {_charaMove.Steering} \n";
        dataPalyer += $"\n";

        dataPalyer += $"Paralyse : {_charaMove.IsParalysed} \n";

        if (_charaMove.TrapResonnance != null)
            dataPalyer += $"Scream : {_charaMove.TrapResonnance.name} \n";
        else
            dataPalyer += $"Scream : Empty \n";

        return dataPalyer;
    }

    string InputData()
    {
        string dataPalyer = "";

        dataPalyer += "F1 : Debug Mode / F2 : Back Ennemy / F3 : Next Ennemy /\n F4 : Back Baby / F5 : Next Baby\n\n";

        //Up
        if (_inputManager.Vertical < 0)
            dataPalyer += $"    x ";
        else
            dataPalyer += $"    o ";
        //Assign
        if (_inputManager.Assign)
            dataPalyer += $"       x \n";
        else
            dataPalyer += $"       o \n";

        //Left
        if (_inputManager.Horizontal < 0)
            dataPalyer += $" x ";
        else
            dataPalyer += $" o ";

        //Down
        if (_inputManager.Vertical > 0)
            dataPalyer += $" x ";
        else
            dataPalyer += $" o ";

        //Right
        if (_inputManager.Horizontal > 0)
            dataPalyer += $" x ";
        else
            dataPalyer += $" o ";

        //Call
        if (_inputManager.Call)
            dataPalyer += $"    xxx \n";
        else
            dataPalyer += $"    ooo \n";

        return dataPalyer;
    }

    string EnnemyData(StateEnnemyController[] ennemy, int iteration)
    {
        string dataEnnemy = "";

        if (iteration > 0 && ennemy.Length > 1)
            dataEnnemy += "<";
        else
            dataEnnemy += " ";

        dataEnnemy += $"- Name : {ennemy[iteration].transform.parent.name} -";

        if (iteration < ennemy.Length - 1 && ennemy.Length > 1)
            dataEnnemy += ">";
        else
            dataEnnemy += " ";

        dataEnnemy += $"\n\n";

        dataEnnemy += $"Position Round : {ennemy[iteration].RoundPositions[ennemy[iteration].Iteration].name} \n";
        dataEnnemy += $"\n";

        dataEnnemy += $"Actual Speed : {GameManager.Instance.ReduceFloatValue(ennemy[iteration].Character.velocity.magnitude, 2)} / {ennemy[iteration].Character.speed} \n";
        dataEnnemy += $"Speed Value : {ennemy[iteration].Speed} \n";
        dataEnnemy += $"Distance See : {ennemy[iteration].DistanceSee} \n";
        dataEnnemy += $"Time No See : {ennemy[iteration].TimeChase} / {ennemy[iteration].TimeSinceNoSee} \n";
        dataEnnemy += $"\n";

        if (ennemy[iteration].Target != null)
            dataEnnemy += $"Target : {ennemy[iteration].Target.name} \n";
        else
            dataEnnemy += $"Target : Empty \n";

        dataEnnemy += $"\n";
        dataEnnemy += $"State : {ennemy[iteration].currentState}\n";

        return dataEnnemy;
    }

    string BabyData(StateBabyController[] baby, int iteration)
    {
        string dataBaby = "";

        if (iteration > 0 && baby.Length > 1)
            dataBaby += "<";
        else
            dataBaby += " ";

        dataBaby += $"- Name : {baby[iteration].transform.parent.name} -";

        if (iteration < baby.Length - 1 && baby.Length > 1)
            dataBaby += ">";
        else
            dataBaby += " ";

        dataBaby += $"\n\n";

        dataBaby += $"Actual Speed : {GameManager.Instance.ReduceFloatValue(baby[iteration].Agent.velocity.magnitude, 2)} / {baby[iteration].Agent.speed} \n";
        dataBaby += $"\n";

        dataBaby += $"Paralyse : {baby[iteration].IsParalysed} \n";
        dataBaby += $"Charges Push : {baby[iteration].Charges} \n";
        dataBaby += $"Distance : {baby[iteration].Distance} \n";
        dataBaby += $"\n";

        dataBaby += $"Transporting : {baby[iteration].isTransporting} \n";
        dataBaby += $"Grab : {baby[iteration].isGoingToGrab}\n";
        dataBaby += $"\n";

        if (baby[iteration].Target != null)
            dataBaby += $"Target Move : {baby[iteration].Target.name} \n";
        else
            dataBaby += $"Target Move : Empty \n";

        if (baby[iteration].TargetObject != null)
            dataBaby += $"Target Object : {baby[iteration].TargetObject.name} \n";
        else
            dataBaby += $"Target Object : Empty \n";

        dataBaby += $"\n";
        dataBaby += $"State : {baby[iteration].currentState}\n";

        return dataBaby;
    }

    string GpeData(ObjectResonnance obj)
    {
        string dataObj = "";

        dataObj += $"---- Name : {obj.transform.name} ---- \n";
        dataObj += $"\n";

        dataObj += $"Target Position : {obj.BabyTarget.name} \n";
        float valueMoveSpeedTarget = ((obj.BabyTarget.transform.right * _charaMove.Position + obj.BabyTarget.transform.forward * _charaMove.Rotation) * obj.SpeedBabyTarget * Time.deltaTime).magnitude;
        dataObj += $"Target Speed : {GameManager.Instance.ReduceFloatValue(valueMoveSpeedTarget, 2)} / {obj.SpeedBabyTarget} \n";
        dataObj += $"\n";

        dataObj += $"Resonating : {obj.IsResonating} \n";
        dataObj += $"Player inside : {obj.IsPlayerInside} \n";

        return dataObj;
    }

    string GpeData(ObjectCollect obj)
    {
        string dataObj = ""; ;

        if (_objCollectController.ObjectToGrab != null)
            dataObj += $"Object To Grab : {_objCollectController.ObjectToGrab.name} \n";
        else
            dataObj += $"Object To Grab : Empty \n";

        dataObj += $"Portable : {obj.IsPortable} \n";
        dataObj += $"Listened : {obj.isListened} \n";

        return dataObj;
    }

    string GpeData(ObjectToPush obj)
    {
        string dataObj = ""; ;

        dataObj += $"Blockers : {obj.CheckBlockers.Length} \n";
        dataObj += $"\n";

        dataObj += $"Baby Actionned : ";
        for (int i = 0; i < obj.IsBabyActionned.Length; i++)
        {
            if (obj.IsBabyActionned[i])
                dataObj += " o";
            else
                dataObj += " x";
        }
        dataObj += $"\nParent Actionned : {obj.IsParentActionned} \n";
        dataObj += $"\n";

        dataObj += $"Value Push : {obj.ValuePush} \n";
        dataObj += $"Decrease Babies Charge : {obj.DecreasePushValue} \n";

        return dataObj;
    }

    void Gpe()
    {
        RaycastHit hit;
        Physics.Raycast(GameManager.Instance.BabyManager.transform.position, GameManager.Instance.BabyManager.transform.TransformDirection(Vector3.back), out hit, GameManager.Instance.BabyManager.DistanceAssign);

        if (hit.transform != null)
        {
            var obj = hit.transform.GetComponentInParent<ObjectResonnance>();

            if (obj != null)
                UpdateText(GpeData(obj), _textMeshPro[3]);

            var obj1 = hit.transform.GetComponentInParent<ObjectCollect>();

            if (obj1 != null)
                UpdateText(GpeData(obj1), _textMeshPro[3]);

            var obj2 = hit.transform.GetComponentInParent<ObjectToPush>();

            if (obj2 != null)
                UpdateText(GpeData(obj2), _textMeshPro[3]);

            if (obj == null && obj1 == null && obj2 == null)
                _textMeshPro[3].text = $"Pas de signal on dirait :( \n";
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(GameManager.Instance.BabyManager.transform.position, GameManager.Instance.BabyManager.transform.TransformDirection(Vector3.back) * GameManager.Instance.BabyManager.DistanceAssign);
    }
}
