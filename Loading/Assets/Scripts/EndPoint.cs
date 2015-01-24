using UnityEngine;

using System.Collections;


public class EndPoint : MonoBehaviour 
{
	public GameObject player;
	public string nextLevel;
	public GameObject spawn;
	public GameObject cameraSpawn;

	// Use this for initialization
	void Start () 
	{
	
	}

	void OnTriggerEnter () 
	{
		player.transform.position = spawn.transform.position;
		Camera.main.transform.position = cameraSpawn.transform.position;

	}
}
