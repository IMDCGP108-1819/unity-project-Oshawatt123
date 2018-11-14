using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class roomTemplates : MonoBehaviour {

	public GameObject[] bottomRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;
	public GameObject[] puzzlesBoiii;

	public GameObject closedRoom;

	public List<GameObject> rooms;

	public float waitTime;
	public bool spawnedBoss;
	public GameObject boss;

	private destroyer Destroyer;

	private GameObject puzzleRoom;
	public bool puzzleSpawned = false;
	public bool helpSpawned;
	private string[] puzzleDoors;

	void Start(){
		Destroyer = GameObject.FindGameObjectWithTag ("Entry").GetComponentInChildren<destroyer> ();
		// hardcoding values for the only puzzle room. will be a switch once more exist
		helpSpawned = false;
		puzzleDoors [0] = "T";
		puzzleDoors [1] = "B";
	}

	void Update(){
		if (waitTime <= 0 && !spawnedBoss) {
			spawnedBoss = true;
			Destroyer.destroyself();
			Instantiate (boss, rooms[rooms.Count-1].transform.position, Quaternion.identity);
			if (rooms.Count >= 8) {
				spawnPuzzle ();
			}
		} else {
			waitTime -= Time.deltaTime;
		}
	}

	void spawnPuzzle(){
		bool canSpawnPuzzle = true;
		puzzleRoom = rooms[Random.Range (5, rooms.Count - 2)];
		foreach (Transform child in puzzleRoom.transform){
			Debug.Log (child.CompareTag ("spawnPoint"));
			if (child.CompareTag("spawnPoint")){
				int openingDir = child.GetComponent<roomSpawner> ().openingDirection;
				if (openingDir == 3 || openingDir == 4){
					canSpawnPuzzle = false;
				}
			}
		}
		Debug.Log (canSpawnPuzzle + " " + puzzleRoom.name);
	}
}