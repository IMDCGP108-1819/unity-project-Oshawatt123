using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letterTileDie : MonoBehaviour {

	public bool killPlayer = true;
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player") && killPlayer) {
			other.gameObject.GetComponent<PlayerMovement>().die ();
		}
	}
}
