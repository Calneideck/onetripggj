﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Car : MonoBehaviour
{
    public GameObject[] packagePrefabs;
    public float accel;
    public float maxSpeed;
    public float rotationSpeed;
    public float initialTime;
    public RawImage currentPackageColourImage;
    public RawImage currentPackageImage;
    public Texture[] colourTextures;
    public Texture[] packageTypeTextures;
    public Texture greyTexture;
    public GameObject[] nextPackageImages;

    public Text scoreText;
    public Text timeText;

    private Rigidbody rb;
    private int packageCount = 0;
    private HouseStruct currentPackage;
    private int score = 0;
    private float startTime;
    private float remainingTime;
    private float bonusTime;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        startTime = Time.time;

        currentPackageColourImage.texture = greyTexture;
        currentPackageImage.gameObject.SetActive(false);
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

        remainingTime = 60 - (Time.time - startTime) + bonusTime;
        timeText.text = "Time: " + Mathf.Ceil(remainingTime);
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
            
            if (packageCount > 0)
                NewPackage();
            else
            {
                currentPackageColourImage.texture = greyTexture;
                currentPackageImage.gameObject.SetActive(false);
            }

            SetNextPackageImages();
        }
    }

    public void ReceivedLetter()
    {
        packageCount++;
        SetNextPackageImages();

        if (packageCount == 1)
            NewPackage();
    }

    void SetNextPackageImages()
    {
        if (packageCount == 1)
        {
            nextPackageImages[0].SetActive(false);
            nextPackageImages[1].SetActive(false);
        }
        else if (packageCount == 2)
        {
            nextPackageImages[0].SetActive(true);
            nextPackageImages[1].SetActive(false);
        }
        else if (packageCount == 3)
        {
            nextPackageImages[0].SetActive(true);
            nextPackageImages[1].SetActive(true);
        }
    }

    public void NewPackage()
    {
        currentPackage = new HouseStruct(true);
        currentPackageColourImage.texture = colourTextures[(int)currentPackage.colour];
        currentPackageImage.gameObject.SetActive(true);
        currentPackageImage.texture = packageTypeTextures[(int)currentPackage.packageType];
    }

    public void PackageDelivered()
    {
        score += 10;
        scoreText.text = "Score: " + score.ToString();
        bonusTime += 15;
    }

    public int PackageCount
    {
        get { return packageCount; }
    }
}
