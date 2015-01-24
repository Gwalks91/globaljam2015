using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public static PlayerMovement Instance;
	public float maxSpeed = 10f;
	private bool pause = false;
	private bool facingRight = true;
	private bool previousFace = true;
	private bool isWalking = false;
	private Animator animator;

	void Awake()
	{
		Instance = this;
		animator = GetComponent<Animator>();
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
			previousFace = facingRight;
			if (move != 0){
				isWalking = true;
				if (move > 0)
					facingRight = true;
				else facingRight = false;
				if (previousFace != facingRight)
					transform.Rotate(0,180,0);
			}
			else isWalking = false;
			animator.SetBool("Walking",isWalking);
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
