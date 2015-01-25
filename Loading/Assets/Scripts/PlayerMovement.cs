using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public static PlayerMovement Instance;
	public float maxSpeed = 10f;
	private bool pause = false;
	private bool isWalking = false;
	private Animator animator;
	private float timeMoving;

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
		float move = Input.GetAxis("Horizontal");
		if (move == 0) {
			isWalking = false;
			timeMoving = timeMoving / (float)1.5;
			if (audio.isPlaying)
					audio.Stop();
		}
		animator.SetBool("Walking",isWalking);
		animator.SetFloat ("timeMoving", timeMoving);
		if(!pause)
		{
			rigidbody.velocity = new Vector3(move * maxSpeed, rigidbody.velocity.y, 0);
			if (move != 0){
				isWalking = true;
				if (timeMoving < 0.95)
					timeMoving += Time.deltaTime / (float)2;
				if (!audio.isPlaying)
					audio.Play();
				Debug.Log ("Audio started playing.");
			}
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
