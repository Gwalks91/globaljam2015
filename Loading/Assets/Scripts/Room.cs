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
		//background = gameObject.AddComponent<SpriteRenderer>();
		//background.sprite = (Sprite)Instantiate(Resources.Load("back1", typeof(Sprite)));
		//backGround = (Image)GameObject.FindObjectOfType(typeof(Image));
		/*
		switch (GameController.Instance.currentState)
		{
		case SANITY.Happy:
			backGround.sprite = (Sprite) AssetsLoader.Instance.BlockSprites(BlockType.Room1Happy);
			break;
		case SANITY.Normal:
			backGround.sprite = (Sprite) AssetsLoader.Instance.BlockSprites(BlockType.Room1Norm);
			break;
		case SANITY.Sad:
			backGround.sprite= (Sprite) AssetsLoader.Instance.BlockSprites(BlockType.Room1Sad);
			break;
		}
		*/
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
		/*
		Sprite newSprite = Resources.Load <Sprite>(newsPaperPath);
		if (newSprite){
			b.image.sprite = newSprite;
		} else {
			Debug.LogError("Sprite not found", this);
		}
		*/
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
