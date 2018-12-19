﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anvilScript : MonoBehaviour {

	public float detectX;
	public float detectY;
	private Vector2 detectSize;

	public LayerMask playerLayer;

	private GameObject AnvilUI;

	// Use this for initialization
	void Start () {
		detectSize = new Vector2 (detectX, detectY);
		AnvilUI = GameObject.Find ("AnvilPopUp");
		AnvilUI.SetActive (false);
	}
	
	void Update(){
		if (Input.GetKeyDown (KeyCode.E)) {
			Collider2D[] nearbyObjects_Anvil = Physics2D.OverlapBoxAll (this.transform.position, detectSize, 0, playerLayer);
			if (nearbyObjects_Anvil.Length > 0) {
				openAnvilUI ();
			}
		}
	}

	private void openAnvilUI(){
		Debug.Log ("open anvil UI");
		AnvilUI.SetActive (true);
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireCube (this.transform.position, new Vector3 (detectX, detectY, 0));
	}
}
