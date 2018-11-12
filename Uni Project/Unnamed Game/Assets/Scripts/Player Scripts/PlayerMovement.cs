using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	//Exposed gameObjects
	//public GameObject enemy;

	//Movement variables
	public Rigidbody2D rigidBody;
	private float moveHor;
	private float moveVer;
	public float moveSpeed;
	public float stamina;
	public float maxStamina;

	public Vector2Int roomID;

	//Roll variables
	public float rollStamina;
	public float rollWait;
	public float rollMod;
	public bool canRoll = true;
	private bool animRolling;
	private bool animAttacking;

	//Animation variables
	public Animator playerAnim;

	//Combat variables
	public float playerHealth;
	public float maxHealth;

	private bool attacking = false;

	//UI Variables
	public Text staminaText;
	public Text healthText;

	// Use this for initialization
	void Start () {
		StartCoroutine (RegenStats ());
		playerHealth = maxHealth;
	}

	void FixedUpdate(){

		// Get RoomID of player
		roomID = new Vector2Int ((Mathf.FloorToInt(this.transform.position.x) / 10) - 20, (Mathf.FloorToInt(this.transform.position.y) / 10) - 19);

		// Animation control
		animAttacking = playerAnim.GetBool("isAttacking");
		animRolling = playerAnim.GetBool ("isRolling");

		//Gets axis values for movement
		float moveHor = Input.GetAxis ("Horizontal");
		float moveVer = Input.GetAxis ("Vertical");

		/*if (Input.GetAxis ("Horizontal") != 0) {
			transform.localScale = new Vector3 (Mathf.Round(Input.GetAxisRaw ("Horizontal")), 0f, 0f);
		}*/

		//apply movement as velocity (keeps collisions going with no acceleration)
		Vector2 movement = new Vector2 (moveHor, moveVer);

		rigidBody.velocity = movement * moveSpeed;

		//Roll
		if (canRoll && !attacking && animAttacking == false && Input.GetAxisRaw ("Roll") == 1 && animRolling == false && stamina > rollStamina && rigidBody.velocity.magnitude > 0) { // fix co-routine
			playerRoll();
		}
		//Add roll force
		if (playerAnim.GetBool ("isRolling") == true) {
			rigidBody.AddForce (rigidBody.velocity * rollMod, ForceMode2D.Impulse);
		}

		//Set UI Text
		staminaText.text = stamina.ToString();
		healthText.text = playerHealth.ToString();

	}

	// ROLL SCRIPTS

	private void playerRoll(){
		stamina -= rollStamina;
		canRoll = false;
		playerAnim.SetBool ("isRolling", true);
	}

	private IEnumerator EndRoll() {
		yield return new WaitForSeconds (0.2f);
		canRoll = true;
	}

	// STAT SCRIPTS

	private IEnumerator RegenStats() {
		while (true) {
			yield return new WaitForSeconds (0.5f);
			stamina = Mathf.Min (stamina + 5, maxStamina);
			playerHealth = Mathf.Min (playerHealth + 2, maxHealth);
		}
	}
}
