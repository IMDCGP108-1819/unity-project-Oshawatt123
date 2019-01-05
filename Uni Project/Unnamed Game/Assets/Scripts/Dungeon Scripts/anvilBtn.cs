using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anvilBtn : MonoBehaviour {

	private GameObject player;

	void Start(){
		player = GameObject.Find ("Player");
	}

	public void closeSelf(){
		GameObject.Find ("AnvilPopUp").SetActive (false);
	}

	public void upgradeSword(){
		if (player.GetComponent<PlayerMovement> ().getCoin() > player.GetComponent<PlayerMovement>().getCoinCost()) {
			GameObject.FindGameObjectWithTag ("Player").GetComponent<playerAttack> ().damage += 3;
			player.GetComponent<PlayerMovement> ().setCoin (-calculateNewCost(player.GetComponent<PlayerMovement>().getCoinCost()));
		}
	}

	public void upgradeHealth(){
		float essenceCost = player.GetComponent<PlayerMovement> ().getEssenceCost ();
		if (player.GetComponent<PlayerMovement> ().getEssence () > essenceCost) {
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ().maxHealth += 7;
			player.GetComponent<PlayerMovement> ().setEssence (-essenceCost);
			player.GetComponent<PlayerMovement> ().setEssenceCost (calculateNewCost(essenceCost));
			GameObject.Find ("Anvil").GetComponent<anvilScript> ().openAnvilUI ();
		}
	}

	private float calculateNewCost(float start){
		return Mathf.Pow (start, start + 1);
	}

}
