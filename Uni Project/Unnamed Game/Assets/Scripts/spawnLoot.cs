using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnLoot : MonoBehaviour {

	public GameObject coin;
	public GameObject essence;

	public float minXForce;
	public float maxXForce;

	public float minYForce;
	public float maxYForce;

	//private GameObject tempObject;

	private List<GameObject> spawnedObjects = new List<GameObject>();

	public void lootSpawn(int Severity){
		for (int i = 0; i < Severity; i++) {
			Debug.Log ("round");
			for (int j = 0; j< Random.Range(0, 4); j++) {
				GameObject tempObject = Instantiate (coin, this.transform.position, Quaternion.identity) as GameObject;
				Vector2 tempForce = new Vector2(Random.Range(minXForce, maxXForce), Random.Range(minYForce, maxYForce));
				tempObject.GetComponent<Rigidbody2D> ().AddForce (tempForce, ForceMode2D.Impulse);
				spawnedObjects.Add(tempObject);
				Debug.Log ("added coin");
			}
			for (int j = 0; j< Random.Range(0, 4); j++) {
				GameObject tempObject = Instantiate (essence, this.transform.position, Quaternion.identity) as GameObject;
				Vector2 tempForce = new Vector2(Random.Range(minXForce, maxXForce), Random.Range(minYForce, maxYForce));
				tempObject.GetComponent<Rigidbody2D> ().AddForce (tempForce, ForceMode2D.Impulse);
				spawnedObjects.Add(tempObject);
				Debug.Log ("Added essence");
			}
		}
		Debug.Log (spawnedObjects);
	}

}
