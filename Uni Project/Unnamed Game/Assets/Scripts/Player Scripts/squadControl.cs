using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squadControl : MonoBehaviour {

	private List<GameObject> squadMembers = new List<GameObject>();

	public void addSquadMember(GameObject squadMember){
		squadMembers.Add (squadMember);
	}
}