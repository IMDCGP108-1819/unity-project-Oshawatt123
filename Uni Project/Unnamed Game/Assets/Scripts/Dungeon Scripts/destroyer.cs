using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyer : MonoBehaviour {

	// this destroyer class is for the destroyer object
	// an issue occurs on dungeon generation where rooms would
	// spawn on top of the main room
	// this is made in order to combat that, so sits in the middle
	// of the main room and destroys room creation objects before they can
	// spawn on top of the main room

	void OnTriggerEnter2D(Collider2D other){
		Destroy (other.gameObject);
	}

	public void destroyself(){
		Destroy (this.gameObject);
	}
}
