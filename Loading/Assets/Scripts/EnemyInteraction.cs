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
		Text[] t = GameObject.FindObjectsOfType(typeof(Text)) as Text[];
		foreach(Text text in t)
		{
			if(left > 0)
			{
				text.text = mtexts[left-1];
				left--;
			}
			else
			{
				text.text = "";
			}
		}
		//GameController.Instance.SendMessage("AddSanity");
	}
}
