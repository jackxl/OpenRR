using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

	public float speed;
	public float tilt;

    public float jumpSpeed;
    public float jumpForce;

    public int maxAllowedJumps;
    public int jumpcount;

    public bool cannotExplode;

    public GameManager gameManager;

	public Rigidbody rb;

    private Vector3 bodyVelocity;

    private int updateCounter = 0;

    public Text centerText;

    GameObject fuelNeedle;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

        gameManager = (GameManager) GameObject.Find("GameManager").GetComponent("GameManager");
        maxAllowedJumps = gameManager.m_trackLengthInpieces;

        cannotExplode = false;

        fuelNeedle = (GameObject) GameObject.Find("needle");
	}
	
    void Update()
    {
        updateCounter++;

        //crashing
        if(rb.velocity.z < 0.5f && updateCounter > 60)
        {
            deleteBody();
        }

        //too high
        if(rb.transform.position.y > 20.0f)
        {
            deleteBody();
        }

        //offroad
        if(rb.transform.position.x > 7 || rb.transform.position.x < -7)
        {
            deleteBody();
        }

        //too low
        if (rb.position.y < 1.21f)
        {
            rb.AddForce(new Vector3(0.0f, 5.0f, 0.0f));
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        var fingerCount = 0;
            foreach (var touch in Input.touches)
            {
                if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                    fingerCount++;
            }
            if (fingerCount > 0)
                Jump();

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, 1);

		rb.AddForce (movement * speed);

        // accellerometer input

        rb.AddForce(new Vector3(Input.acceleration.x * 20, 0.0f));

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, 0.0f);

        if(rb.position.y < -1)
        {
            deleteBody();
        }

        bodyVelocity = rb.velocity;
        if((rb.velocity.z - bodyVelocity.z > 1.0f))
        {
            deleteBody();
        }
	}

    public void Jump()
    {
        if(cannotExplode)
        {
            SceneManager.LoadScene(0);
        }

        if(rb.transform.position.y < 1.25f)
        {
            if (jumpcount >= maxAllowedJumps)
            {
                return;
            }
            var v3 = new Vector3(0.0f, 20.0f * jumpForce, 0.0f);

            rb.AddForce(v3);
            jumpcount++;


            //the fuel gage
            int fuelLevel = (maxAllowedJumps - jumpcount) / maxAllowedJumps;
            double startRot = 26;
            float totalSwing = 124;

            fuelNeedle.transform.Rotate(new Vector3(0, 0, totalSwing / maxAllowedJumps));
        }
    }

    public void deleteBody()
    {
        if (!cannotExplode)
        {
            Destroy(rb.gameObject);
        }
    }
}
