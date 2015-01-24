using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public string nextScreen;
	public void changeLevel()
	{
		Application.LoadLevel(nextScreen);
	}
}
