using UnityEngine;

using System.Collections;


public class EndPoint : MonoBehaviour 
{

	void OnTriggerEnter () 
	{
		GameController.Instance.SendMessage("changeRoom");
	}
}
