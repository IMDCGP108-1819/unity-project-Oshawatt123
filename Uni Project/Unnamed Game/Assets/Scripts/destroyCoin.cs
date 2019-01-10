using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyCoin : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other){
		//Debug.Log ("fnworfno");
		if (other.gameObject.CompareTag ("Player")) {
			//Debug.Log ("Touchy Coin");
			other.gameObject.GetComponent<PlayerMovement> ().setCoin (1);
			Destroy (this.gameObject);
		}
	}
}
