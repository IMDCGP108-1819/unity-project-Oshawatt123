using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Basic : MonoBehaviour {

	// Attached components
	public Transform hitBoxTransform;
	public Rigidbody2D rigidBody;

	//Combat Variables
	public float health;
	public float knockbackMod;

	// Movement Variables
	private GameObject player;
	private Vector2 playerPos;
	private Vector2 toPlayer;

	private float mag;
	private Vector2 toPlayerUnit;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		playerPos = new Vector2 (player.transform.position.x, player.transform.position.y);
		toPlayer = playerPos- new Vector2(this.transform.position.x, this.transform.position.y);

		//Conditional movement
		// stop moving once near enough to the player to attack, then attack!!!
		// move only when seen the player (AND awake)
		// asleep enemies will be woken on contact (movement by player collision)
		if (Mathf.Abs(toPlayer.x) > 1.5 || Mathf.Abs(toPlayer.y) > 1.5) {
			mag = Mathf.Sqrt (toPlayer.x * toPlayer.x + toPlayer.y * toPlayer.y);
			toPlayerUnit = toPlayer / mag;
			rigidBody.velocity = toPlayerUnit;
		}

		hitBoxTransform.localPosition = new Vector3 (0, 0, 0);
		if (health <= 0) {
			Debug.Log ("Ded m8");
			Destroy (this.gameObject);
		}
	}

	public void takeDamage(float damage){
		// apply an impulse force vector2(skeleton.position - player.position)
		rigidBody.AddForce (-toPlayer * knockbackMod, ForceMode2D.Impulse);
		health -= damage;
		Debug.Log ("Ouch!");
	}
}