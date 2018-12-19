using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anvilBtn : MonoBehaviour {

	public void closeSelf(){
		GameObject.Find ("AnvilPopUp").SetActive (false);
	}

	public void upgradeSword(){
		GameObject.FindGameObjectWithTag ("Player").GetComponent<playerAttack> ().damage += 3;
	}

	public void upgradeHealth(){
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ().maxHealth += 7;
	}
}
