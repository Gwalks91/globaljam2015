using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float maxSpeed = 10f;
	private bool facingRight = true;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		float move = Input.GetAxis("Horizontal");

		rigidbody.velocity = new Vector3(move * maxSpeed, rigidbody.velocity.y, 0);
	}


}
