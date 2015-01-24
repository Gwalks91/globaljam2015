using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


public class LoadXmlData : MonoBehaviour // the Class
{
	public TextAsset GameAsset;
	private string mroomName;

	public void init(string roomName)
	{
		mroomName = roomName;
		GetRoom();
	}

	public void GetRoom()
	{
		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		xmlDoc.LoadXml(GameAsset.text); // load the file.
		XmlNodeList roomsList = xmlDoc.GetElementsByTagName("Room"); // array of the level nodes.
		
		foreach (XmlNode roomInfo in roomsList)
		{
			if(roomInfo.Attributes["roomName"].Value == mroomName)
			{
				//add the numChoice to the room so we can set up the text objects for the ui
				Debug.Log(roomInfo.Attributes["roomName"].Value);
				Debug.Log(mroomName);
				gameObject.GetComponent<EnemyInteraction>().setNumChoice(int.Parse(roomInfo.Attributes["numChoices"].Value));
				XmlNodeList roomcontent = roomInfo.ChildNodes;
				
				foreach (XmlNode roomsItens in roomcontent) // levels itens nodes.
				{
					Debug.Log(roomsItens.Attributes["text"].Value);
					gameObject.GetComponent<EnemyInteraction>().addString(roomsItens.Attributes["text"].Value,
					                                                      int.Parse(roomsItens.Attributes["score"].Value),
					                                                      roomsItens.Attributes["finalImage"].Value);
					//Set up the differnet UI texts with strings and scores
				}
			}

		}
	}
}
