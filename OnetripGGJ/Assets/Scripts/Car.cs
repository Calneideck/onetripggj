using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Car : MonoBehaviour
{
    public GameObject[] packagePrefabs;
    public float accel;
    public float maxSpeed;
    public float rotationSpeed;
    public float initialTime;
    public int timeBonus;
    public RawImage currentPackageColourImage;
    public RawImage currentPackageImage;
    public Texture[] colourTextures;
    public Texture[] packageTypeTextures;
    public Texture greyTexture;
    public GameObject[] nextPackageImages;
    public Text countdownText;
    public Text scoreText;
    public Text timeText;
    public AudioClip ding;

    private Rigidbody rb;
    private int packageCount = 0;
    private HouseStruct currentPackage;
    private int score = 0;
    private float gameStartTime;
    private float countdownStartTime;
    private float countdownTime;
    private float remainingTime;
    private float bonusTime;
    private bool started = false;
    private bool gameOver = false;
    private bool go = false;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = transform.Find("Audio Source").GetComponent<AudioSource>();
        countdownStartTime = Time.time;

        currentPackageColourImage.texture = greyTexture;
        currentPackageImage.gameObject.SetActive(false);

        timeText.text = "Time: " + Mathf.Ceil(initialTime);
    }
    	
    void Update()
    {
        if (started)
        {
            // Steering
            if (Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") < 0)
                transform.Rotate(transform.up, -rotationSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") > 0)
                transform.Rotate(transform.up, rotationSpeed * Time.deltaTime);

            if (Input.GetButtonDown("Drop"))
                DropLetter();

            GameTimer();
        }
        else
        {
            // Countdown timer
            countdownTime = 3 - (Time.time - countdownStartTime);
            countdownText.text = Mathf.Ceil(countdownTime).ToString();
            countdownText.fontSize = Mathf.RoundToInt(200 * (countdownTime - Mathf.Floor(countdownTime)));

            if (countdownTime <= 0)
            {
                started = true;
                countdownText.text = "Go!";
                gameStartTime = Time.time;
            }
        }
    }

	void FixedUpdate ()
    {
        if (!started)
            return;

        // Forward and reverse movement

        if (Input.GetKey(KeyCode.W) || Input.GetButton("Accel"))
            rb.AddForce(transform.forward * accel);

        if (Input.GetKey(KeyCode.S) || Input.GetButton("Rev"))
            rb.AddForce(transform.forward * -1 * accel);

        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    void GameTimer()
    {
        remainingTime = initialTime - (Time.time - gameStartTime) + bonusTime;

        if (remainingTime < 0)
        {
            remainingTime = 0;

            if (!gameOver)
                GameOver();
        }

        timeText.text = "Time: " + Mathf.Ceil(remainingTime);

        // Countdown timer after 0 has been reached - ( GO! )
        if (countdownTime >= -1)
        {
            countdownTime = 3 - (Time.time - countdownStartTime);
            countdownText.fontSize = Mathf.RoundToInt(200 * (countdownTime + 1));
        }
        else if (!go)
        {
            go = true;
            countdownText.gameObject.SetActive(false);
        }
    }

    void GameOver()
    {
        GameObject.Find("UI").GetComponent<ShowPanels>().EndScreen(true, score);
        gameOver = true;
        nextPackageImages[0].SetActive(false);
        nextPackageImages[1].SetActive(false);
        currentPackageImage.gameObject.SetActive(false);
        currentPackageColourImage.gameObject.SetActive(false);
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

            // Get new package info unless none left then display grey circle
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

    // This is for the small icons that indicate how many packages you are carrying
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
        bonusTime += timeBonus;

        audioSource.volume = 0.2f;
        audioSource.clip = ding;
        audioSource.Play();
    }

    public int PackageCount
    {
        get { return packageCount; }
    }
}
