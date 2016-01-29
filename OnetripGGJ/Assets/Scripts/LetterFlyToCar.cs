using UnityEngine;
using System.Collections;

public class LetterFlyToCar : MonoBehaviour
{
    public float flyingTime;
    public float flySpeed;

    private Transform car;
    private bool flying = false;
    private float startTime;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetCar(Transform car)
    {
        // set the car transform and setup for flying as well as giving the letter a bit of spin

        this.car = car;
        flying = true;
        startTime = Time.time;
        GetComponent<Rigidbody>().AddTorque(Vector3.one, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        // Move the letter towards the car
        if (flying)
            rb.velocity = (car.position - transform.position).normalized * flySpeed;
    }
}
