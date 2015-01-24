using UnityEngine;
using System.Collections.Generic;

public enum BlockType
{
	Room1Sad = 0,
	Room1Happy = 1,
	Room1Norm = 2,
	Room2Sad = 3,
	Room2Happy = 4,
	Room2Norm = 5,
	Room3Sad = 6,
	Room3Happy = 7,
	Room3Norm = 8,
	Room4Sad = 9,
	Room4Happy = 10,
	Room4Norm = 11,
	Room5Sad = 12,
	Room5Happy = 13,
	Room5Norm = 14
}

public class AssetsLoader : MonoBehaviour
{
	public static AssetsLoader Instance;
	[SerializeField]
	private GameObject[] blockPrefabs;

	void Awake() 
	{
		Instance = this;
	}

	public GameObject BlockSprites(BlockType blockType) {
		return blockPrefabs[(int)blockType];
	}
}