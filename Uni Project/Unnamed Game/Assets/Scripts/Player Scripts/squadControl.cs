using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squadControl : MonoBehaviour {

	private List<GameObject> squadMembers = new List<GameObject>();

	// lets other scripts add squad members to the list
	public void addSquadMember(GameObject squadMember){
		squadMembers.Add (squadMember);
		Debug.Log ("added to list");
	}

	void Start(){
		Debug.Log ("oh my!");
	}

	public void newFloor(Vector3 floorLocation){
		// reset for new floor
		//Debug.Log ("no: " + squadMembers.Count);
		foreach (GameObject squadMember in squadMembers) {
			Debug.Log ("moving squad member");
			squadMember.transform.position = new Vector3(floorLocation.x + 2, floorLocation.y - 2, 0f);
		}
	}
}