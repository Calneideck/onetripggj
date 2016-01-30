﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Car : MonoBehaviour
{
    public GameObject[] packagePrefabs;
    public float accel;
    public float maxSpeed;
    public float rotationSpeed;
    public Text packageCountText;
    public Text currentPackageText;
    public Text scoreText;

    private Rigidbody rb;
    private int packageCount = 0;
    private HouseStruct currentPackage;
    private int score = 0;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
    void Update()
    {
        // Steering

        if (Input.GetKey(KeyCode.A))
            transform.Rotate(transform.up, -rotationSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(transform.up, rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
            DropLetter();
    }

	void FixedUpdate ()
    {
        // Forward and reverse movement

        if (Input.GetKey(KeyCode.W))
            rb.AddForce(transform.forward * accel);

        if (Input.GetKey(KeyCode.S))
            rb.AddForce(transform.forward * -1 * accel);

        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    void DropLetter()
    {
        if (packageCount > 0)
        {
            // create the letter, giving it a bit of spin and turning triggers off so it collides with the world
            int rnd = Random.Range(0, packagePrefabs.Length);
            GameObject letter = (GameObject)GameObject.Instantiate(packagePrefabs[rnd], transform.position + Vector3.down * 1.5f, Random.rotation);
            letter.GetComponent<Rigidbody>().AddTorque(Vector3.one, ForceMode.Impulse);

            if (rnd == 0)
                letter.transform.Find("Mesh").GetComponent<MeshCollider>().isTrigger = false;
            else
                letter.GetComponent<MeshCollider>().isTrigger = false;

            letter.GetComponent<Letter>().Setup(gameObject, currentPackage);

            packageCount--;
            packageCountText.text = "Packages: " + packageCount.ToString();

            // If the last package was dropped, display 'No Package'
            if (packageCount > 0)
                NewPackage();
            else
                currentPackageText.text = "No Package";
        }
    }

    public void ReceivedLetter()
    {
        packageCount++;
        packageCountText.text = "Packages: " + packageCount.ToString();

        if (packageCount == 1)
            NewPackage();
    }

    public void NewPackage()
    {
        currentPackage = new HouseStruct(true);
        currentPackageText.text = currentPackage.colour.ToString() + " - " + currentPackage.packageType.ToString();
    }

    public void PackageDelivered()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }
}
