using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letterTileDie : MonoBehaviour {

	//For the puzzle tiles that aren't correct. These will kill the player when collided with.

	// This variable allows me to add this script to all puzzle tiles and select whether it
	// should kill the player or not. Allows for fast prototyping
	public bool killPlayer = true;
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player") && killPlayer) {
			other.gameObject.GetComponent<PlayerMovement>().die ();
		}
	}
}
