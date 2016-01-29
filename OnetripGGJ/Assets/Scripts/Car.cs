using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour
{
    public GameObject letterPrefab;
    public float speed;
    public float maxSpeed;
    public float rotationSpeed;

    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(transform.up, -rotationSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(transform.up, rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
            DropLetter();
    }

	void FixedUpdate ()
    {
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(transform.forward * speed);

        if (Input.GetKey(KeyCode.S))
            rb.AddForce(transform.forward * -1 * speed);

        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    void DropLetter()
    {
        GameObject.Instantiate(letterPrefab, transform.position + Vector3.down, Quaternion.identity);
    }
}
