using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnLoot : MonoBehaviour {

	public GameObject coin;
	public GameObject essence;

	//private GameObject tempObject;

	private List<GameObject> spawnedObjects = new List<GameObject>();

	public void lootSpawn(int Severity){
		for (int i = 0; i < Severity; i++) {
			Debug.Log ("round");
			for (int j = 0; j< Random.Range(0, 4); j++) {
				GameObject tempObject = Instantiate (coin) as GameObject;
				spawnedObjects.Add(tempObject);
				Debug.Log ("added coin");
			}
			for (int j = 0; j< Random.Range(0, 4); j++) {
				GameObject tempObject = Instantiate (essence, this.transform.position, Quaternion.identity) as GameObject;
				spawnedObjects.Add(tempObject);
				Debug.Log ("Added essence");
			}
		}
		Debug.Log (spawnedObjects);
	}

}
