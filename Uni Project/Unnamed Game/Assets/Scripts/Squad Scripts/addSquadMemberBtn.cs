using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addSquadMemberBtn : MonoBehaviour {

	public void addSquadMember(){
		GameObject.FindGameObjectWithTag("cage").GetComponent<squadSpawn> ().addSquadMember();
	}

	public void closeSelf(){
		GameObject.Find ("SquadRecruitPop-Up").SetActive (false);
		GameObject.FindGameObjectWithTag ("cage").GetComponent<squadSpawn> ().addingMember = false;
	}

}