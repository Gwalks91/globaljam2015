using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public static GameController Instance;
	
	void Awake() 
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void AddSanity()
	{
		Debug.Log("Message got here");
	}
}
