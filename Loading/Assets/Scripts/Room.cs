using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Room : MonoBehaviour 
{
	public GameObject AI;
	public GameObject AI1;
	public GameObject AI2;
	public GameObject AI3;
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
		if(roomName == "room2")
		{
			GameObject.Find("train").renderer.enabled = true;
			GameObject.Find("tracks").renderer.enabled = true;
			GameObject.Find("Station").renderer.enabled = true;
		}
		if(roomName == "room3")
		{
			GameObject.Find("Store").renderer.enabled = true;
		}
		AI2.SetActive(false);
		AI1.SetActive(false);
		AI3.SetActive(false);
		misActive = isActive;
		mRoomName = roomName;
		if(!misActive)
		{
			Debug.Log ("Made an inactive room: " + roomName);
			AI2.SetActive(false);
			AI1.SetActive(false);
			AI3.SetActive(false);
			//GameObject.Find("body1").renderer.enabled = false;
			//GameObject.Find("body2").renderer.enabled = false;
			//GameObject.Find("body3").renderer.enabled = false;
		}
		else
		{

			Debug.Log ("Made an active room: " + roomName);
			//AI.renderer.enabled = true;
			if(roomName == "room1")
			{
				AI1.SetActive(true);
				//AI1.renderer.enabled = true;
				AI2.SetActive(false);
				AI3.SetActive(false);
				AI1.GetComponentInChildren<EnemyInteraction>().init ();
				AI1.GetComponentInChildren<LoadXmlData>().init(mRoomName);
			}
			else if(roomName == "room2")
			{
				AI2.SetActive(true);
				//AI2.renderer.enabled = true;
				AI1.SetActive(false);
				AI3.SetActive(false);
				AI2.GetComponentInChildren<EnemyInteraction>().init ();
				AI2.GetComponentInChildren<LoadXmlData>().init(mRoomName);
			}
			else if(roomName == "room3")
			{
				AI3.SetActive(true);
				//AI3.renderer.enabled = true;
				AI1.SetActive(false);
				AI2.SetActive(false);
				AI3.GetComponentInChildren<EnemyInteraction>().init ();
				AI3.GetComponentInChildren<LoadXmlData>().init(mRoomName);
			}

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
