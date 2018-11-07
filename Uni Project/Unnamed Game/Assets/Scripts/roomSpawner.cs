using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomSpawner : MonoBehaviour {

	public int openingDirection;
	// 1 needs bottom door
	// 2 needs top door
	// 3 needs left door
	// 4 needs right door

	private roomTemplates templates;

	private int rand;

	private bool spawned = false;

	void Start(){
		templates  = GameObject.FindGameObjectWithTag("Rooms").GetComponent<roomTemplates>();
		Invoke ("Spawn", 0.2f);
	}

	void Spawn(){
		if (spawned == false) {
			if (openingDirection == 1) {
				// spawn a room with bottom opening
				rand = Random.Range (0, templates.bottomRooms.Length);
				Instantiate (templates.bottomRooms [rand], transform.position, templates.bottomRooms [rand].transform.rotation);
			} else if (openingDirection == 2) {
				// room with top
				rand = Random.Range (0, templates.topRooms.Length);
				Instantiate (templates.topRooms [rand], transform.position, templates.topRooms [rand].transform.rotation);
			} else if (openingDirection == 3) {
				// room with left
				rand = Random.Range (0, templates.leftRooms.Length);
				Instantiate (templates.leftRooms [rand], transform.position, templates.leftRooms [rand].transform.rotation);
			} else if (openingDirection == 4) {
				// room with right
				rand = Random.Range (0, templates.rightRooms.Length);
				Instantiate (templates.rightRooms [rand], transform.position, templates.rightRooms [rand].transform.rotation);
			}
			spawned = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("spawnPoint")) {
			if (other.GetComponent<roomSpawner> ().spawned == false && spawned == false) {
				Instantiate (templates.closedRoom, transform.position, Quaternion.identity);
				Destroy (gameObject);
			}
			spawned = true;
		}
	}
}
