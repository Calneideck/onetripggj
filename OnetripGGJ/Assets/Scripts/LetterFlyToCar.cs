using UnityEngine;
using System.Collections;

public class LetterFlyToCar : MonoBehaviour
{
    public float flySpeed;

    private Transform car;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetCar(Transform car)
    {
        // set the car transform and setup for flying as well as giving the letter a bit of spin

        this.car = car;
        GetComponent<Rigidbody>().AddTorque(Vector3.one, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        // Move the letter towards the car
        if (car != null)
            rb.velocity = (car.position - transform.position).normalized * flySpeed;
    }
}
