using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destrotyEssence : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other){
		//Debug.Log ("fwrgwfqad");
		if (other.gameObject.CompareTag ("Player")) {
			//Debug.Log ("Touchy Essence");
			other.gameObject.GetComponent<PlayerMovement> ().setEssence (1);
			Destroy (this.gameObject);
		}
	}

}