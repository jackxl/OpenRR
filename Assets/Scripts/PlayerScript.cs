using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float maxSpeedKMH;
	public float speedIncresement; 

	private float currentSpeed;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		currentSpeed = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizonatal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
	
		rb.AddForce (movement * 10);
	}
}
