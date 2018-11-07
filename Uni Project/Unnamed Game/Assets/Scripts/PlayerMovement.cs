using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	//Exposed gameObjects
	public GameObject enemy;

	//Movement variables
	public Rigidbody2D rigidBody;
	private float moveHor;
	private float moveVer;
	public float moveSpeed;
	public float stamina;
	public float maxStamina;

	//Roll variables
	public float rollStamina;
	public float rollWait;
	public float rollMod;
	public bool canRoll = true;
	private bool animRolling;

	//Animation variables
	public Animator playerAnim;

	//Combat variables
	public float playerHealth;
	public float maxHealth;

	public GameObject swordHB;
	public float swordDMG;
	private bool attacking = false;
	private bool animAttack;
	private bool canAttack = true;

	//UI Variables
	public Text staminaText;

	// Use this for initialization
	void Start () {
	}

	void FixedUpdate(){

		animAttack = playerAnim.GetBool ("isAttacking");
		animRolling = playerAnim.GetBool ("isRolling");

		//Gets axis values for movement
		float moveHor = Input.GetAxis ("Horizontal");
		float moveVer = Input.GetAxis ("Vertical");

		/*if (Input.GetAxis ("Horizontal") != 0) {
			transform.localScale = new Vector3 (Mathf.Round(Input.GetAxisRaw ("Horizontal")), 0f, 0f);
		}*/

		Vector2 movement = new Vector2 (moveHor, moveVer);

		rigidBody.velocity = movement * moveSpeed; //apply movement as velocity (keeps collisions going)

		//Roll
		if (canRoll && !attacking && Input.GetAxisRaw ("Roll") == 1 && animRolling == false && animAttack == false && stamina > 20 && rigidBody.velocity.magnitude > 0) { // fix co-routine
			playerRoll();
		}
		//Add roll force
		if (playerAnim.GetBool ("isRolling") == true) {
			rigidBody.AddForce (rigidBody.velocity * rollMod, ForceMode2D.Impulse);
		}

		//Set UI Text
		staminaText.text = stamina.ToString();

	}

	// ROLL SCRIPTS

	private void playerRoll(){
		stamina -= rollStamina;
		canRoll = false;
		canAttack = false;
		playerAnim.SetBool ("isRolling", true);
	}

	private IEnumerator EndRoll() {
		canAttack = true;
		yield return new WaitForSeconds (0.2f);
		canRoll = true;
	}

	// STAT SCRIPTS

	private IEnumerator RegenStats() {
		yield return new WaitForSeconds (0.1f);
		stamina = Mathf.Min (stamina + 5, maxStamina);
		playerHealth = Mathf.Min (playerHealth + 2, maxHealth);
	}
}
