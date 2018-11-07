using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Basic : MonoBehaviour {

	public Transform hitBoxTransform;

	//Combat Variables
	public float health;

	// Use this for initialization
	void Start () {
		//create simple "walk-to" AI
		//create paths for enemy patrols
		//create enemy line of sight
	}
	
	// Update is called once per frame
	void Update () {
		hitBoxTransform.localPosition = new Vector3 (0, 0, 0);
	}

	public void takeDamage(float damage){
		health -= damage;
		Debug.Log ("Ouch!");
	}
}
