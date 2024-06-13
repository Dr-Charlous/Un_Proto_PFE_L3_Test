using UnityEngine;

public class PlayerMeshFollow : MonoBehaviour
{
    [SerializeField] Transform _pivotCam;
    [SerializeField] float _lerpDistance = 0.25f;
    [SerializeField] float _lerpRotate = 0.25f;

    Vector3 _velocity;

    private void LateUpdate()
    {
        transform.position = _pivotCam.position;
        //transform.position = Vector3.Lerp(transform.position, _pivotCam.position, 0.5f);

        //Vector3.SmoothDamp(transform.position, _pivotCam.position, ref _velocity, 0.1f);
        //transform.LookAt(_pivotCam.position);

        //var direction = Camera.main.transform.forward;
        //direction.y = 0f;

        Vector3 direction = GameManager.Instance.Character.Rb.velocity.normalized;

        if (direction != Vector3.zero || GameManager.Instance.Character.Rb.velocity.magnitude < 0.15f)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }
}
