using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour 
{
	public static GameController Instance;
	public GameObject player;
	public GameObject currentRoom;
	public SpriteRenderer backGround_sad;
	public SpriteRenderer backGround_normal;
	public SpriteRenderer backGround_happy;
    public AudioClip newsSound;
    public AudioClip moodHappySound;
    public AudioClip moodSadSound;
    public int maxRooms;
    private AudioSource newsSE;
    private AudioSource BGM;
	private int currentRoomNum;
	private int currentActiveRoomNum;
	private int sanity;
	private List<int> roomsLeft;
    private bool inEndOfDay;

	
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
		for(int i = 1; i < maxRooms; i++)
			roomsLeft.Add(i);
		int activeRoom = Random.Range(0, roomsLeft.Count-1);
		Debug.Log(activeRoom);
		currentActiveRoomNum = roomsLeft[activeRoom];
		roomsLeft.RemoveAt(activeRoom);
		for(int i = 0; i<roomsLeft.Count; i++)
			Debug.Log(roomsLeft[i]);

		backGround_sad.renderer.enabled = false;
		backGround_normal.renderer.enabled = true;
		backGround_happy.renderer.enabled = false;
		//backGround_normal = (Texture)Resources.Load ("background_normal.png");
        foreach(GameObject o in GameObject.FindGameObjectsWithTag("UserChoice"))
        {
            Button b = o.GetComponent<Button>();
            if (b  != null)
            {
                b.enabled = false;
                b.image.enabled = false;
                b.GetComponentInChildren<Text>().enabled = false;
            }
        }

		Text t = GameObject.FindGameObjectWithTag("Explanation").GetComponent<Text>();
		t.text = "";
		t.enabled = false;

		foreach (GameObject o in GameObject.FindGameObjectsWithTag("EOD"))
        {
            Button b = o.GetComponent<Button>();
            if (b != null)
            {
                b.enabled = false;
                b.image.enabled = false;
            }
        }
        //foreach(Button b in GameObject.FindObjectsOfType(typeof(Button)) as Button[])
        //{
        //    b.enabled = false;
        //    b.image.enabled = false;
        //    b.GetComponentInChildren<Text>().enabled = false;
        //    //b.renderer.enabled = false;
        //}
		GameObject.Find("bridge").renderer.enabled = false;
        inEndOfDay = false;

        AudioSource[] ass = GetComponents<AudioSource>();

        BGM = ass[0];
        BGM.clip = moodHappySound;
        BGM.loop = true;
        BGM.playOnAwake = true;
        BGM.Play();

        newsSE = ass[1];
        newsSE.clip = newsSound;
        newsSE.loop = false;
        newsSE.playOnAwake = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (sanity <= 0)
		{
			Debug.Log ("End Game");//end the game 
			Application.LoadLevel("GameOver");
		}

        if (inEndOfDay)
        {

        }
	}

	void AddSanity(int toChange)
	{
		Debug.Log("Message got here");
		sanity += toChange;
		Debug.Log("Sanity: " + sanity);
	}

	void changeRoom()
	{
		GameObject.Find("bridge").renderer.enabled = false;
		GameObject.Find("housePoly").renderer.enabled = false;
		if(currentRoomNum == maxRooms-1)
		{
			reset();
		}
		else
		{
			Debug.Log("Switching to room: " + currentRoomNum);
			currentRoomNum++;
			currentRoom.GetComponent<Room>().init(currentRoomNum == currentActiveRoomNum, "room" + currentRoomNum);
			player.transform.position = currentRoom.GetComponent<Room>().getStartPos();
		}
	}

	void reset()
	{
		//Show end image
		GameObject.Find("housePoly").renderer.enabled = true;
		currentRoomNum = 0;
		if(roomsLeft.Count > 0)
		{
			inEndOfDay = true;
            newsSE.PlayOneShot(newsSound);
			PlayerMovement.Instance.SendMessage("togglePause");
			foreach (GameObject o in GameObject.FindGameObjectsWithTag("EOD"))
			{
				Button b = o.GetComponent<Button>();
				if (b != null)
				{
					b.enabled = true;
					b.image.enabled = true;
					b.onClick.AddListener(() => onEODButtonClick());
				}
			}

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
            // win screen because you survived all the events possible

            inEndOfDay = true;
            newsSE.PlayOneShot(newsSound);
            PlayerMovement.Instance.SendMessage("togglePause");
            foreach (GameObject o in GameObject.FindGameObjectsWithTag("EOD"))
            {
                Button b = o.GetComponent<Button>();
                if (b != null)
                {
                    b.enabled = true;
                    b.image.enabled = true;
                    b.onClick.AddListener(() => onEODButtonClick());
                }
            }
			player.transform.position = currentRoom.GetComponent<Room>().getStartPos();
			Application.LoadLevel("WinScreen");
		}

		if(sanity > 5)
		{
			backGround_sad.renderer.enabled = false;
			backGround_normal.renderer.enabled = false;
			backGround_happy.renderer.enabled = true;

            BGM.Stop();
            BGM.clip = moodHappySound;
            BGM.Play();
		}
		else if(sanity <= 5 && sanity > 2)
        {
			backGround_sad.renderer.enabled = false;
			backGround_normal.renderer.enabled = true;
			backGround_happy.renderer.enabled = false;

            BGM.Stop();
            BGM.clip = moodHappySound;
            BGM.Play();
		}
		else
        {
			backGround_sad.renderer.enabled = true;
			backGround_normal.renderer.enabled = false;
			backGround_happy.renderer.enabled = false;

            BGM.Stop();
            BGM.clip = moodSadSound;
            BGM.Play();
		}
	}

    private void onEODButtonClick()
    {
        PlayerMovement.Instance.SendMessage("togglePause");
        GameObject[] gos = GameObject.FindGameObjectsWithTag("EOD");
        foreach(GameObject o in gos)
        {
            Button b = o.GetComponent<Button>();
            if(b != null)
            {
                b.image.enabled = false;
                b.enabled = false;
                b.onClick.RemoveAllListeners();
            }
        }
    }
}
