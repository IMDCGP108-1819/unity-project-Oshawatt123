using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	//Player Coinage
	public float coins;
	public float coinCost = 3f;
	public float essences;
	public float essenceCost = 3f;

	public Text coinText;
	public Text essenceText;

	//Movement variables
	public Rigidbody2D rigidBody;
	private Vector2 movement = new Vector2 (0f, 0f);
	private float moveHor;
	private float moveVer;
	public float moveSpeed;
	public float stamina;
	public float maxStamina;
	private bool canMove = true;

	public Vector2Int roomID;

	//Roll variables
	public float rollStamina;
	public float rollWait;
	public float rollMod;
	public bool canRoll = true;
	private bool animRolling;

	//Animation variables
	public Animator playerAnim;

	//Combat variables
	public playerAttack attackScript;
	public float playerHealth;
	public float maxHealth;
	private bool attacking = false;
	public float attackTime;

	public float spawnX;
	public float spawnY;

	//UI Variables
	public Text staminaText;
	public Text healthText;
	public Image deathFadeScreen;

	// Floor Variables
	private int floor;

	public Camera mainCamera;

	// Use this for initialization
	void Start () {
		canMove = false;
		StartCoroutine (resetMove ());
		// Prep for persistence of the player
		//DontDestroyOnLoad (this.gameObject);
		deathFadeScreen.gameObject.SetActive (true);
		deathFadeScreen.CrossFadeAlpha (0f, 3f, true);
		StartCoroutine (RegenStats ());
		playerHealth = maxHealth;
	}

	void FixedUpdate(){

		if (Input.GetKey (KeyCode.R)) {
			mainCamera.orthographicSize = 15f;
		} else {
			mainCamera.orthographicSize = 2.5f;
		}

		// Get RoomID of player
		roomID = new Vector2Int ((Mathf.FloorToInt(this.transform.position.x) / 10) - 20, (Mathf.FloorToInt(this.transform.position.y) / 10) - 19);

		// Animation control
		//animAttacking = playerAnim.GetBool("isAttacking");
		animRolling = playerAnim.GetBool ("isRolling");

		//Gets axis values for movement
		float moveHor = Input.GetAxis ("Horizontal");
		float moveVer = Input.GetAxis ("Vertical");

		// Flip the character based off of movement
		Vector3 newScale = transform.localScale;
		if (Input.GetAxisRaw("Horizontal") != 0){
			newScale.x = Input.GetAxisRaw ("Horizontal") * -1;
		}
		transform.localScale = newScale;

		/*if (Input.GetAxis ("Horizontal") != 0) {
			transform.localScale = new Vector3 (Mathf.Round(Input.GetAxisRaw ("Horizontal")), 0f, 0f);
		}*/

		//apply movement as velocity (keeps collisions going with no acceleration)
		if (canMove) {
			movement = new Vector2 (moveHor, moveVer);
			playerAnim.SetFloat ("speed", movement.magnitude);
		}

		rigidBody.velocity = movement * moveSpeed;

		//Attack
		if (Input.GetAxisRaw ("Attack") == 1) {
			attackScript.attack (attackTime, this.gameObject);
		}

		//Roll
		if (canRoll && !attacking && Input.GetAxisRaw ("Roll") == 1 && animRolling == false && stamina > rollStamina && rigidBody.velocity.magnitude > 0) { // fix co-routine
			playerRoll();
			rigidBody.AddForce (rigidBody.velocity * rollMod, ForceMode2D.Impulse);
		}

		//Set UI Text
		staminaText.text = "Stamina: " + stamina.ToString();
		healthText.text = "Health: " + playerHealth.ToString();
		coinText.text = "Coins: " + coins.ToString ();
		essenceText.text = "Essence: " + essences.ToString ();

		attackTime -= Time.deltaTime;
	}

	/*void LateUpdate(){
		float absValue = Mathf.Abs (rigidBody.velocity);
		if (absValue > 0) {
			this.transform.localScale.x = 1;
		} else if (absValue < 0) {
			this.transform.localScale.x = -1;
		}

	}*/

	private IEnumerator resetMove(){
		yield return new WaitForSeconds (5f);
		canMove = true;
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

	// DAMAGE SCRIPTS

	public void takeDamage(float damage){
		Debug.Log ("Is this happening?");
		playerHealth -= damage;
	}

	public void die(){
		StartCoroutine (death ());
	}

	private IEnumerator death(){
		// fades in a 'death screen' to hide the player being teleported back to the start
		movement = new Vector2 (0f, 0f);
		canMove = false;
		deathFadeScreen.CrossFadeAlpha (1f, 2f, true);
		yield return new WaitForSeconds (2f);
		this.transform.position = new Vector3 (spawnX, spawnY, 0f);
		deathFadeScreen.CrossFadeAlpha (0f, 2f, true);
		canMove = true;
	}

	// STAT SCRIPTS

	private IEnumerator RegenStats() {
		while (true) {
			yield return new WaitForSeconds (0.5f);
			stamina = Mathf.Min (stamina + 2, maxStamina);
			playerHealth = Mathf.Min (playerHealth + 1, maxHealth);
		}
	}

	//Coinage Functions - getters and setters (more like adders) because they really shouldn't be public

	//Costs are stored in the player and not the anvil because the anvil does not persist through floors,
	//but the player does so can keep track of this. If dealt with earlier, I could have made a seperate
	//object or script to store all values (like costs and currencies) for the player

	public void setCoin(float coinage){
		coins += coinage;
		Debug.Log (coins);
	}

	public float getCoin(){
		return coins;
	}

	public float getCoinCost(){
		return coinCost;
	}

	public void setCoinCost(float newCoinCost){
		coinCost = newCoinCost;
	}

	public void setEssence(float essencage){
		essences += essencage;
		Debug.Log ("added essence");
	}

	public float getEssence(){
		return essences;
	}

	public float getEssenceCost(){
		return essenceCost;
	}

	public void setEssenceCost(float newEssenceCost){
		essenceCost = newEssenceCost;
	}

	// pause and death helper function
	public void setCanMove(bool boolie){
		canMove = boolie;
	}

	// new floor function
	public void newFloor(Vector3 floorLocation){
		// implement the fade thingy
		canMove = false;
		this.transform.position = new Vector3(floorLocation.x + 1, floorLocation.y - 1, 0);
	}

}