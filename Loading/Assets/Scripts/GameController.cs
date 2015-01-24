using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public enum SANITY
{
	Happy = 0,
	Normal = 1,
	Sad = 2
}

public class GameController : MonoBehaviour 
{
	public static GameController Instance;
	public GameObject player;
	public GameObject currentRoom;
	//public SpriteRenderer backGround_sad;
	//public SpriteRenderer backGround_normal;
	//public SpriteRenderer backGround_happy;
	public int maxRooms;
	public SANITY currentState;
	private int currentRoomNum;
	private int currentActiveRoomNum;
	private int sanity;
	private List<int> roomsLeft;

	
	void Awake() 
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		roomsLeft = new List<int>();
		currentRoom.GetComponent<Room>().init(false, 0);
		player = GameObject.FindGameObjectWithTag("Player");
		player.transform.position = currentRoom.GetComponent<Room>().getStartPos();

		currentRoomNum = 0;
		sanity = 1;
		for(int i = 1; i < maxRooms-1; i++)
			roomsLeft.Add(i);
		int activeRoom = Random.Range(0, roomsLeft.Count-1);
		Debug.Log(activeRoom);
		currentActiveRoomNum = roomsLeft[activeRoom];
		roomsLeft.RemoveAt(activeRoom);
		for(int i = 0; i<roomsLeft.Count; i++)
			Debug.Log(roomsLeft[i]);

		//backGround_sad.renderer.enabled = false;
		//backGround_normal.renderer.enabled = true;
		//backGround_happy.renderer.enabled = false;
		//backGround_normal = (Texture)Resources.Load ("background_normal.png");
		foreach(Button b in GameObject.FindObjectsOfType(typeof(Button)) as Button[])
		{
			b.enabled = false;
			b.image.enabled = false;
			b.GetComponentInChildren<Text>().enabled = false;
			//b.renderer.enabled = false;
		}

		currentState = SANITY.Normal;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (sanity <= 0)
		{
			Application.LoadLevel("GameOver");
			Debug.Log ("End Game");//end the game 
		}
	}

	void AddSanity(int toChange)
	{
		Debug.Log("Message got here");
		sanity += toChange;

	}

	void changeRoom()
	{
		if(currentRoomNum == maxRooms-1)
		{
			reset();
		}
		else
		{
			currentRoomNum++;
			currentRoom.GetComponent<Room>().init(currentRoomNum == currentActiveRoomNum, currentRoomNum);
			player.transform.position = currentRoom.GetComponent<Room>().getStartPos();
		}
	}

	void reset()
	{
		//Show end image

		currentRoomNum = 0;
		if(roomsLeft.Count > 0)
		{
			int activeRoom = Random.Range(0, roomsLeft.Count-1);
			Debug.Log(activeRoom);
			currentActiveRoomNum = roomsLeft[activeRoom];
			roomsLeft.RemoveAt(activeRoom);
			for(int i = 0; i < roomsLeft.Count; i++)
				Debug.Log(roomsLeft[i]);
			player.transform.position = currentRoom.GetComponent<Room>().getStartPos();
		}
		else
		{
			Debug.Log ("END!!!!");
			Application.LoadLevel("WinScreen");
			player.transform.position = currentRoom.GetComponent<Room>().getStartPos();
		}

		if(sanity > 5)
		{
			currentState = SANITY.Happy;
		}
		else if(sanity <= 5 && sanity > 2)
		{
			currentState = SANITY.Normal;
		}
		else
		{
			currentState = SANITY.Sad;
		}
	}
}
