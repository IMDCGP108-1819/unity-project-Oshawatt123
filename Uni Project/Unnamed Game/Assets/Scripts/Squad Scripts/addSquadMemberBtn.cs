using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addSquadMemberBtn : MonoBehaviour {

	public void addSquadMember(){
		// adds the squad member to the list
		GameObject.FindGameObjectWithTag("cage").GetComponent<squadSpawn> ().addSquadMember();
	}

	public void closeSelf(){
		// close the UI
		GameObject.Find ("SquadRecruitPop-Up").SetActive (false);
		GameObject.FindGameObjectWithTag ("cage").GetComponent<squadSpawn> ().addingMember = false;
	}

}