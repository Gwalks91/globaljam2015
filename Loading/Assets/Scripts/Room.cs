using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour 
{
	public GameObject AI;
	public GameObject EndPoint;
	public GameObject StartPoint;

	public string mRoomName;
	private bool misActive;

	public void init(bool isActive, string roomName)
	{

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
