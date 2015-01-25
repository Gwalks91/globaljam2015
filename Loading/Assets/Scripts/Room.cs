using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Room : MonoBehaviour 
{
	public GameObject AI;
	public GameObject EndPoint;
	public GameObject StartPoint;
	
	public GameObject background;

	public string mRoomName;
	private bool misActive;
	private string backgroundPath;
	private Sprite newSprite;

	public void init(bool isActive, string roomName)
	{
		if(roomName == "room1")
		{
			GameObject.Find("bridge").renderer.enabled = true;
		}
		if(roomName == "room0")
		{
			GameObject.Find("housePoly").renderer.enabled = true;
		}
		if(roomName == "room3")
		{
			GameObject.Find("Store").renderer.enabled = true;
		}

		misActive = isActive;
		mRoomName = roomName;
		if(!misActive)
		{
			Debug.Log ("Made an inactive room: " + roomName);
			AI.SetActive(false);
			AI.renderer.enabled = false;
		}
		else
		{
			Debug.Log ("Made an active room: " + roomName);
			AI.renderer.enabled = true;
			AI.SetActive(true);
			AI.GetComponentInChildren<EnemyInteraction>().init ();
			AI.GetComponentInChildren<LoadXmlData>().init(mRoomName);
		}

		switch (GameController.Instance.currentMood)
		{
		case STATUS.normal:
			GameObject.Find("Rain").GetComponent<ParticleEmitter>().emit = false;
			backgroundPath = "BackGrounds/" + mRoomName + "norm";
			break;
		case STATUS.sad:
			GameObject.Find("Rain").GetComponent<ParticleEmitter>().emit = true;
			backgroundPath = "BackGrounds/" + mRoomName + "sad";
			break;
		case STATUS.happy:
			GameObject.Find("Rain").GetComponent<ParticleEmitter>().emit = false;
			backgroundPath = "BackGrounds/" + mRoomName + "happy";
			break;
		default:
			GameObject.Find("Rain").GetComponent<ParticleEmitter>().emit = false;
			backgroundPath = "";
			break;

		}
		newSprite = Resources.Load <Sprite>(backgroundPath);
		if (newSprite){
			GameObject temp = GameObject.Find("BackGround");
			temp.GetComponent<SpriteRenderer>().sprite = newSprite;
		} else {
			Debug.LogError("Sprite not found", this);
		}

	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector3 getStartPos()
	{
		return StartPoint.transform.position;
	}
}
