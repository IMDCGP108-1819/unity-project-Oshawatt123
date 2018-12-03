using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destrotyEssence : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("fwrgwfqad");
		if (other.CompareTag ("Player")) {
			Debug.Log ("Touchy Essence");
			other.GetComponent<PlayerMovement> ().addEssence ();
			Destroy (this.gameObject);
		}
	}

}