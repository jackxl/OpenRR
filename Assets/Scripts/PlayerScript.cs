using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float speed;
	public float tilt;

    public float jumpSpeed;
    public float jumpForce;

	private Rigidbody rb;

    private Vector3 bodyVelocity;

    private int updateCounter = 0;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
    void Update()
    {
        updateCounter++;

        if(rb.velocity.z < 0.5f && updateCounter > 60)
        {
            deleteBody();
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

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, 1);

		rb.AddForce (movement * speed);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);

        if(rb.position.y < -1)
        {
            deleteBody();
        }

        bodyVelocity = rb.velocity;
        if(rb.velocity.z - bodyVelocity.z > 1.0f)
        {
            deleteBody();
        }
	}

    public void Jump()
    {
        if(rb.transform.position.y < 2.5f)
        {
            var v3 = new Vector3(0.0f, 20.0f * jumpForce, 0.0f);

            rb.AddForce(v3);
        }
    }

    public void deleteBody()
    {
        Destroy(rb.gameObject);
    }
}
