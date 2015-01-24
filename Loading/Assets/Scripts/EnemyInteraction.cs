using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemyInteraction : MonoBehaviour 
{
	private int mnumChoices;
	private List<string> mtexts;
	private Dictionary<string, int> mchoiceCost;
	private Dictionary<string, string> mchoiceImage;

	// Use this for initialization
	void Start () 
	{

	}

	public void init()
	{
		mtexts = new List<string>();
		mchoiceCost = new Dictionary<string, int>();
		mchoiceImage = new Dictionary<string, string>();
	}

	public void setNumChoice(int numChoices)
	{
		mnumChoices = numChoices;
	}

	public void addString(string text, int value, string image)
	{
		mtexts.Add(text);
		mchoiceCost.Add(text, value);
		mchoiceImage.Add(text, image);
	}

	// Update is called once per frame
	void OnTriggerEnter () 
	{
		Debug.Log("Player hit the AI");
		int left = mnumChoices;
		PlayerMovement.Instance.SendMessage("togglePause");
		Button[] t = GameObject.FindObjectsOfType(typeof(Button)) as Button[];
		foreach(Button b in t)
		{
			if(left > 0)
			{
				b.enabled = true;
				b.image.enabled = true;
				b.GetComponentInChildren<Text>().enabled = true;
				b.GetComponentInChildren<Text>().text = mtexts[left-1];
				Debug.Log (mtexts[left-1]);
				Debug.Log (left);
				string text = mtexts[left-1];
				b.onClick.AddListener(() => onButtonClick(text));
				left--;
			}
			else
			{
				b.GetComponentInChildren<Text>().text = "";
			}
		}
		//GameController.Instance.SendMessage("AddSanity");
	}

	void onButtonClick(string buttonName)
	{
		Debug.Log("This is causing an error: " + buttonName);
		Debug.Log("This is causing an error: " + mchoiceCost[buttonName]);
		GameController.Instance.SendMessage("AddSanity", mchoiceCost[buttonName]);
		PlayerMovement.Instance.SendMessage("togglePause");
		Button[] t = GameObject.FindObjectsOfType(typeof(Button)) as Button[];
		foreach(Button b in t)
		{
			b.image.enabled = false;
			b.enabled = false;
			b.onClick.RemoveAllListeners();
			b.GetComponentInChildren<Text>().enabled = false;
		}


	}
}
