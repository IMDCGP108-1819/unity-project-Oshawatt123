using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomTemplates : MonoBehaviour {

	public GameObject[] bottomRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;

	public GameObject closedRoom;

	public List<GameObject> rooms;

	public float waitTime;
	private bool spawnedBoss;
	public GameObject boss;

	private destroyer Destroyer;

	void Start(){
		Destroyer = GameObject.FindGameObjectWithTag ("Entry").GetComponentInChildren<destroyer> ();
	}

	void Update(){
		if (waitTime <= 0 && !spawnedBoss) {
			Instantiate (boss, rooms[rooms.Count-1].transform.position, Quaternion.identity);
			Destroyer.destroyself ();
			spawnedBoss = true;
		} else {
			waitTime -= Time.deltaTime;
		}
	}
}
