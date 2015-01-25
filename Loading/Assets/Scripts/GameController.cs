using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public enum STATUS
{
	normal = 0,
	sad = 1, 
	happy = 2
}

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
	public string newsPaperPath;
	public STATUS currentMood;

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
		newsPaperPath = "";
		roomsLeft = new List<int>();
		currentRoom.GetComponent<Room>().init(false, "room0");
		player = GameObject.FindGameObjectWithTag("Player");
		player.transform.position = currentRoom.GetComponent<Room>().getStartPos();
		GameObject.Find("Rain").GetComponent<ParticleEmitter>().emit = false;
		currentRoomNum = 0;
		sanity = 5;
		currentMood = STATUS.normal;
		for(int i = 1; i < maxRooms; i++)
			roomsLeft.Add(i);
		int activeRoom = Random.Range(0, roomsLeft.Count);
		Debug.Log(activeRoom);
		currentActiveRoomNum = roomsLeft[activeRoom];
		roomsLeft.RemoveAt(activeRoom);
		for(int i = 0; i<roomsLeft.Count; i++)
			Debug.Log(roomsLeft[i]);
		
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
		GameObject.Find("TextBackGround").GetComponent<Image>().enabled = false;

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
		GameObject.Find("Store").renderer.enabled = false;
		GameObject.Find("train").renderer.enabled = false;
		GameObject.Find("tracks").renderer.enabled = false;
		GameObject.Find("Station").renderer.enabled = false;
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

	public void changePath(string newPath)
	{
		newsPaperPath = newPath;
	}

	void changeRoom()
	{
		GameObject.Find("bridge").renderer.enabled = false;
		GameObject.Find("housePoly").renderer.enabled = false;
		GameObject.Find("Store").renderer.enabled = false;
		GameObject.Find("train").renderer.enabled = false;
		GameObject.Find("tracks").renderer.enabled = false;
		GameObject.Find("Station").renderer.enabled = false;
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
		if(sanity > 5)
		{
			currentMood = STATUS.happy;
			
			BGM.Stop();
			BGM.clip = moodHappySound;
			BGM.Play();
		}
		else if(sanity <= 5 && sanity > 2)
		{
			currentMood = STATUS.normal;
			
			BGM.Stop();
			BGM.clip = moodHappySound;
			BGM.Play();
		}
		else
		{
			currentMood = STATUS.sad;
			
			BGM.Stop();
			BGM.clip = moodSadSound;
			BGM.Play();
		}
		//Show end image
		//GameObject.Find("housePoly").renderer.enabled = true;
		currentRoomNum = 0;
		currentRoom.GetComponent<Room>().init(currentRoomNum == currentActiveRoomNum, "room" + currentRoomNum);
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
					Sprite newSprite = Resources.Load <Sprite>(newsPaperPath);
					if (newSprite){
						b.image.sprite = newSprite;
					} else {
						Debug.LogError("Sprite not found: " + newsPaperPath, this);
					}
					b.onClick.AddListener(() => onEODButtonClick(false));
				}
			}

			int activeRoom = Random.Range(0, roomsLeft.Count);
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
					Sprite newSprite = Resources.Load <Sprite>(newsPaperPath);
					if (newSprite){
						b.image.sprite = newSprite;
					} else {
						Debug.LogError("Sprite not found", this);
					}
					b.image.enabled = true;
                    b.onClick.AddListener(() => onEODButtonClick(true));
                }
            }
			player.transform.position = currentRoom.GetComponent<Room>().getStartPos();

		}


	}

    private void onEODButtonClick(bool lastScene)
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
		if(lastScene)
			Application.LoadLevel("WinScreen");
    }
}
