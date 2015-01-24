using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour 
{
	public static GameController Instance;
	public GameObject player;
	public GameObject currentRoom;
	public Sprite backGround_sad;
	public Sprite backGround_normal;
	public Sprite backGround_happy;
	public int maxRooms;
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
		currentRoom.GetComponent<Room>().init(false, "room0");
		player = GameObject.FindGameObjectWithTag("Player");
		player.transform.position = currentRoom.GetComponent<Room>().getStartPos();

		currentRoomNum = 0;
		sanity = 5;
		for(int i = 1; i < maxRooms-1; i++)
			roomsLeft.Add(i);
		int activeRoom = Random.Range(0, roomsLeft.Count-1);
		Debug.Log(activeRoom);
		currentActiveRoomNum = roomsLeft[activeRoom];
		roomsLeft.RemoveAt(activeRoom);
		for(int i = 0; i<roomsLeft.Count; i++)
			Debug.Log(roomsLeft[i]);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (sanity <= 0)
			Debug.Log ("End Game");//end the game 
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
			currentRoom.GetComponent<Room>().init(currentRoomNum == currentActiveRoomNum, "room" + currentRoomNum);
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
			for(int i = 0; i<roomsLeft.Count; i++)
				Debug.Log(roomsLeft[i]);
			player.transform.position = currentRoom.GetComponent<Room>().getStartPos();
		}
		else
		{
			Debug.Log ("END!!!!");
			player.transform.position = currentRoom.GetComponent<Room>().getStartPos();
		}
	}
}
