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
		// grab the coinCost variables from the player as it is used multiple times
		float coinCost = player.GetComponent<PlayerMovement> ().getCoinCost ();
		if (player.GetComponent<PlayerMovement> ().getCoin() > coinCost) { // check player has enough coins
			GameObject.FindGameObjectWithTag ("Player").GetComponent<playerAttack> ().damage += 3; // increment their damage stat
			player.GetComponent<PlayerMovement> ().setCoin (-coinCost); // charge the player for their purchase
			player.GetComponent<PlayerMovement> ().setCoinCost (calculateNewCost(coinCost)); // set the new coin cost in the player
			GameObject.Find ("Anvil").GetComponent<anvilScript> ().openAnvilUI (); // re-open the AnvilUI (does not create 2 copies) so the values are refreshed in the UI
		}
	}

	//Costs are stored in the player and not the anvil because the anvil does not persist through floors,
	//but the player does so can keep track of this. If dealt with earlier, I could have made a seperate
	//object or script to store all values (like costs and currencies) for the player

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
		return start + 2;
	}

}
