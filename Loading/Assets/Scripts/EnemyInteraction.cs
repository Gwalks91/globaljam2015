using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyInteraction : MonoBehaviour 
{
	public Canvas ui;
	public Text choice1;
	public Text choice2;

	// Use this for initialization
	void Start () 
	{
		ui.enabled = false;
	}
	
	// Update is called once per frame
	void OnTriggerEnter () 
	{
		ui.enabled = true;
		Debug.Log("Player hit the AI");
		choice1.text = "Hello";
		choice2.text = "Good Bye";
		GameController.Instance.SendMessage("AddSanity");
	}
}
