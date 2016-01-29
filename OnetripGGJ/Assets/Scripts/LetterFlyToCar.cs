using UnityEngine;
using System.Collections;

public class LetterFlyToCar : MonoBehaviour
{
    public float flyingTime;

    private Transform car;
    private Rigidbody rb;
    private bool flying = false;
    private float startTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetCar(Transform car)
    {
        this.car = car;
        Invoke("StartFlying", 0);
    }

    void StartFlying()
    {
        flying = true;
        startTime = Time.time;
    }

    void Update()
    {
        if (flying)
        {
            Vector3 pos = transform.position;

            float t = (Time.time - startTime) / flyingTime;

            pos = Vector3.Lerp(pos, car.position, t);
            transform.position = pos;
        }
    }
}
