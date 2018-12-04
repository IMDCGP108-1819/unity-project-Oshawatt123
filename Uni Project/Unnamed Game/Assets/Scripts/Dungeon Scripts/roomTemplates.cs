using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class roomTemplates : MonoBehaviour {

	public GameObject[] bottomRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;
	public GameObject[] puzzleRooms;

	public GameObject closedRoom;

	public List<GameObject> rooms;

	public float waitTime;
	public bool spawnedBoss;
	private bool killedBoos;
	public GameObject boss;
	public GameObject cage;

	private destroyer Destroyer;

	private GameObject puzzleRoom;
	public GameObject puzzleHelpObject;
	public bool puzzleSpawned = false;
	public bool helpSpawned;
	private int[] puzzleDoors = {0,0,0,0};
	private int numOfDoors;
	private int puzzleRoomIndex;

	void Start(){
		Destroyer = GameObject.FindGameObjectWithTag ("Entry").GetComponentInChildren<destroyer> ();
		// hardcoding values for the only puzzle room. will be a switch once more exist
		helpSpawned = false;
		puzzleDoors [0] = 1;
		puzzleDoors [1] = 2;
		puzzleDoors [2] = 66;
		puzzleDoors [3] = 66;
		numOfDoors = 2;
	}

	void Update(){
		if (waitTime <= 0 && !spawnedBoss) {
			if (rooms.Count >= 8) {
				spawnPuzzle ();
			}


			// SWITCH THE BOSS SPAWNY THING TO ONE MORE LIKE THE PUZZLE AND HAVE SOME PRE-SET BOSS ROOMS
			// WORKS BETTER AND MORE FUN FOR THE PLAYER


			spawnedBoss = true;
			Destroyer.destroyself();
			Instantiate (boss, rooms[rooms.Count-1].transform.position, Quaternion.identity);
			Vector3 cagePos = new Vector3 (rooms [rooms.Count - 1].transform.position.x - 1, rooms [rooms.Count - 1].transform.position.y - 1, -1);
			Instantiate (cage,cagePos, Quaternion.identity);
		} else {
			waitTime -= Time.deltaTime;
		}
	}

	void spawnPuzzle(){
		Debug.Log (puzzleDoors [0]);
		bool puzzleFound = false;
		int numOfCorrectDoors = 0;
		for (int i = rooms.Count-2; i > 3; i--) {
			if (!puzzleFound) {
				puzzleRoom = rooms [i];
				puzzleRoomIndex = i;
			}
			Debug.Log (puzzleRoom);
			foreach (Transform child in puzzleRoom.transform) {
				if (child.CompareTag ("spawnPoint") && !puzzleFound) {
					int openingDir = child.GetComponent<roomSpawner> ().openingDirection;
					Debug.Log (openingDir);
					if (openingDir == puzzleDoors [0] || openingDir == puzzleDoors [1] || openingDir == puzzleDoors [2] || openingDir == puzzleDoors [3]) {
						numOfCorrectDoors += 1;
					}
				}
				if (numOfCorrectDoors == numOfDoors) {
					puzzleFound = true;
				}
			}
			numOfCorrectDoors = 0;
		}
		if (puzzleFound) {
			Instantiate(puzzleHelpObject, rooms[Random.Range(0, puzzleRoomIndex-1)].transform.position, Quaternion.identity);
			Instantiate (puzzleRooms [0], puzzleRoom.transform.position, Quaternion.identity);
			Destroy (puzzleRoom);
		}
		Debug.Log ("Room: " + puzzleRoom.name);
	}
}