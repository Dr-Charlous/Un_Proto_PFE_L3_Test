using UnityEngine;

public class PlayerMeshFollow : MonoBehaviour
{
    [SerializeField] Transform _pivotCam;
    [SerializeField] float _lerpDistance = 0.25f;
    [SerializeField] float _lerpRotate = 0.25f;

    Vector3 _velocity;
    Quaternion _lastRotation;

    private void LateUpdate()
    {
        transform.position = _pivotCam.position;
        //transform.position = Vector3.Lerp(transform.position, _pivotCam.position, 0.5f);

        //Vector3.SmoothDamp(transform.position, _pivotCam.position, ref _velocity, 0.1f);
        //transform.LookAt(_pivotCam.position);

        //var direction = Camera.main.transform.forward;
        //direction.y = 0f;

        Vector3 direction = GameManager.Instance.Character.transform.right * GameManager.Instance.Character.Rotation + -GameManager.Instance.Character.transform.forward * GameManager.Instance.Character.Position;
        //Vector3 direction2 = (new Vector3((transform.right * GameManager.Instance.Character.Rotation).z, 0, (transform.right * -GameManager.Instance.Character.Rotation).x) + -transform.forward * GameManager.Instance.Character.Position).normalized;

        //Debug.Log($"{direction}\n{direction2}");

        if ((GameManager.Instance.Character.Rotation != 0 || GameManager.Instance.Character.Position != 0) && !GameManager.Instance.Character.IsParalysed)
        {
            _lastRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction.normalized), 0.1f);
        }
        else
            _lastRotation = transform.rotation;

        transform.rotation = _lastRotation;
    }
}
