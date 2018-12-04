using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squadScript : MonoBehaviour {

	private float state;
	// 1 = idle
	// 2 = following
	// 3 = engaged in combat

	public GameObject player;

	// generic variables
	private float playerDistance = 0f;
	public Rigidbody2D rigidBody;
	public float speed;

	// idle variables
	public float followRange;

	// follow variables

	// engagement variables

	// Use this for initialization
	void Start () {
		state = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == 1) {
			idle ();
		} else if (state == 2) {
			follow ();
		} else if (state == 3) {
			engaged ();
		}
	}

	private float getDistance(GameObject object1, GameObject object2){
		Vector3 diffVector = object2.transform.position - object1.transform.position;
		return (Mathf.Sqrt(Mathf.Pow(diffVector.x, 2) + Mathf.Pow(diffVector.y, 2)));
	}

	private void idle(){
		playerDistance = getDistance (this.gameObject, player);
		if (playerDistance > followRange) {
			state = 2;
		}// else if enemy in attack range
		//else if player in close follow range
	}

	private void follow(){
		playerDistance = getDistance (this.gameObject, player);
		if (playerDistance < followRange) {
			state = 1;
			rigidBody.velocity = new Vector2(0,0);
		} else {
			rigidBody.velocity = ((player.transform.position - this.transform.position) / playerDistance) * speed;
		}
	}

	private void engaged(){

	}
}