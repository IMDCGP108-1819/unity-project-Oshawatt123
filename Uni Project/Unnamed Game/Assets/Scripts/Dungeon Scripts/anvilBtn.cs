using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anvilBtn : MonoBehaviour {

	private GameObject player;

	public float coinCost;
	public float essenceCost;

	void Start(){
		player = GameObject.Find ("Player");
	}

	public void closeSelf(){
		GameObject.Find ("AnvilPopUp").SetActive (false);
	}

	public void upgradeSword(){
		if (player.GetComponent<PlayerMovement> ().getCoin() > coinCost) {
			GameObject.FindGameObjectWithTag ("Player").GetComponent<playerAttack> ().damage += 3;
			player.GetComponent<PlayerMovement> ().setCoin (-coinCost);
		}
	}

	public void upgradeHealth(){
		if (player.GetComponent<PlayerMovement> ().getCoin () > essenceCost) {
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ().maxHealth += 7;
			player.GetComponent<PlayerMovement> ().setEssence (-essenceCost);
		}
	}
}
