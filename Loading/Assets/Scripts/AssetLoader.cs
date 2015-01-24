using UnityEngine;
using System.Collections.Generic;

public class AssetsLoader : MonoBehaviour
{
	public GameObject[] blockPrefabs;
	public Dictionary<string, Texture2D> blockTypes;
	
	// ...
	
	void Start () 
	{
		blockTypes = new Dictionary<string, Sprite>();
		
		foreach(GameObject prefab in blockPrefabs) {
			blockTypes[prefab.name] = prefab;
		}
	}
}
