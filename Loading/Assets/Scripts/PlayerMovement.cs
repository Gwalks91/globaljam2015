using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public static PlayerMovement Instance;
	public float maxSpeed = 10f;
	private bool pause = false;
	private bool facingRight = true;

	void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(!pause)
		{
			float move = Input.GetAxis("Horizontal");

			rigidbody.velocity = new Vector3(move * maxSpeed, rigidbody.velocity.y, 0);
		}
	}

	public void togglePause()
	{
		pause = !pause;

        if (pause)
        {
            // make it stop
            rigidbody.velocity = new Vector3(0, 0, 0);
        }
	}


}
