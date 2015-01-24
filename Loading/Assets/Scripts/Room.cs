using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Room : MonoBehaviour 
{
	public GameObject AI;
	public GameObject EndPoint;
	public GameObject StartPoint;

	public SpriteRenderer background;

	public string mRoomName;
	private bool misActive;

	public void init(bool isActive, int roomName)
	{
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
		mRoomName = "room" + roomName;
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
		Debug.Log ("Start Pos: " + StartPoint.transform.position);
		return StartPoint.transform.position;
	}
}
