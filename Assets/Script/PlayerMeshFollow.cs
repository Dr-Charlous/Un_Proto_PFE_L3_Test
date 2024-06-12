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
        
        //Vector3.SmoothDamp(transform.position, _pivotCam.position, ref _velocity, 0.1f);
        //transform.LookAt(_pivotCam.position);

        var direction = Camera.main.transform.forward;
        direction.y = 0f;

        transform.rotation =  Quaternion.Slerp(  transform.rotation,   Quaternion.LookRotation(direction) , 0.1f);
    }
}
