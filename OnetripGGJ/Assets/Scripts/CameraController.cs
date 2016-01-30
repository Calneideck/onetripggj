using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float rotateSpeed;
    public Transform car;

    void LateUpdate()
    {
        // Make the camera smoothly follow the rotation of the car
        transform.position = car.position;
        transform.Translate(car.forward * -5, Space.World);
        Vector3 pos = transform.position;
        pos.y = car.position.y + 2;
        transform.position = pos;
        Vector3 carpos = car.eulerAngles;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(20, carpos.y, 0), rotateSpeed * Time.deltaTime);
    }
}
