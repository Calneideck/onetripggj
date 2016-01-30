using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float snapbackTime;
    public float rotateSpeed;
    public float snapbackSpeed;
    public Transform car;

    private float lastMoveTime;
    private bool moving = false;
    private float slerpTime;

    void LateUpdate()
    {
        transform.position = car.position;
        transform.Translate(car.forward * -5, Space.World);
        Vector3 pos = transform.position;
        pos.y = car.position.y + 2;
        transform.position = pos;

        Vector3 carpos = car.eulerAngles;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(20, carpos.y, 0), rotateSpeed * Time.deltaTime);

    }

    //void Update()
    //{
    //    float mouseX = Input.GetAxis("Mouse X");

    //    //if ((mouseX < 0 && transform.localRotation.y > 270) || (mouseX > 0 && transform.localRotation.y < 90))
    //    if ((mouseX < 0) || (mouseX > 0))
    //    {
    //        transform.Rotate(new Vector3(0, mouseX * rotateSpeed * Time.deltaTime, 0), Space.World);

    //        Vector3 rot = transform.localEulerAngles;

    //        if (rot.y > 180)
    //            rot.y = Mathf.Clamp(rot.y, 270, 360);
    //        else if (rot.y < 180)
    //            rot.y = Mathf.Clamp(rot.y, 0, 90);

    //        transform.localEulerAngles = rot;
    //    }

    //    if (mouseX == 0 && moving)
    //    {
    //        lastMoveTime = Time.time;
    //        slerpTime = 0;
    //        moving = false;
    //    }
    //    else if ((mouseX < 0 || mouseX > 0) && !moving)
    //    {
    //        moving = true;
    //    }

    //    if ((Time.time - lastMoveTime > snapbackTime) && !moving)
    //    {
    //        slerpTime += Time.deltaTime / snapbackSpeed;

    //        Quaternion rot = transform.localRotation;
    //        rot = Quaternion.Slerp(rot, Quaternion.Euler(20, 0, 0), slerpTime);
    //        transform.localRotation = rot;
    //    }
    //}
}
