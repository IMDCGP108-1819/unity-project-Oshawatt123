﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squadScript : MonoBehaviour {

	private float state;
	// 1 = idle
	// 2 = following
	// 3 = engaged in combat

	public GameObject player;

	// generic variables
	public float maxHealth;
	private float health;
	private float playerDistance = 0f;
	public Rigidbody2D rigidBody;
	public float speed;
	private Vector2Int roomID;

	public playerAttack attackScript;

	// idle variables
	public float followRange;

	// follow variables

	// engagement variables
	public float engageRadius;
	public float attackRadius;
	public LayerMask enemyLayer;
	private Collider2D[] nearbyEnemies;
	private GameObject current_target = null;
	private float enemyDistance;

	public float attackTime;

	// Use this for initialization
	void Start () {
		state = 1;
	}
	
	// Update is called once per frame
	void Update () {
		roomID = new Vector2Int ((Mathf.FloorToInt(this.transform.position.x) / 10) - 20, (Mathf.FloorToInt(this.transform.position.y) / 10) - 19);

		checkNearbyEnemies ();
		if (current_target != null) {
			state = 3;
		} else {state = 2;}

		if (state == 1) {
			idle ();
		} else if (state == 2) {
			follow ();
		} else if (state == 3) {
			engaged ();
		}

		attackTime -= Time.deltaTime;

	}

	private float getDistance(GameObject object1, GameObject object2){
		Vector3 diffVector = object2.transform.position - object1.transform.position;
		return (Mathf.Sqrt(Mathf.Pow(diffVector.x, 2) + Mathf.Pow(diffVector.y, 2)));
	}

	private void checkNearbyEnemies(){
		nearbyEnemies = Physics2D.OverlapCircleAll (this.transform.position, engageRadius, enemyLayer);
		foreach (Collider2D enemy in nearbyEnemies){
			//Vector2Int enemyRoomID = enemy.GetComponent<Enemy_Basic> ().getRoomID();
			if (enemy.GetComponent<Enemy_Basic> ().getRoomID() == roomID) {
				current_target = enemy.gameObject;
			}
		}
		Debug.Log (nearbyEnemies.Length);
	}

	private void idle(){
		playerDistance = getDistance (this.gameObject, player);
		if (playerDistance > followRange) {
			state = 2;
		}
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
		Debug.Log ("engaged in combat");
		enemyDistance = getDistance (this.gameObject, current_target);
		rigidBody.velocity = ((current_target.transform.position - this.transform.position) / enemyDistance) * speed;
		if (enemyDistance < attackRadius) {
			attackScript.attack (attackTime, this.gameObject);
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere (this.transform.position, engageRadius);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (this.transform.position, attackRadius);
	}
}