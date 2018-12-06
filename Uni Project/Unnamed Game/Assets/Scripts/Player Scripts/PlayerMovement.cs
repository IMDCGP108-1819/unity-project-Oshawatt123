using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	//Player Coinage
	private float coins;
	private float essences;

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
	private bool animAttacking;

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

	//Player Squadage
	private List<GameObject> squad;

	//UI Variables
	public Text staminaText;
	public Text healthText;
	public Image deathFadeScreen;

	// Use this for initialization
	void Start () {

		// Prep for persistence of the player
		//DontDestroyOnLoad (this.gameObject);
		squad = new List<GameObject>();
		deathFadeScreen.gameObject.SetActive (true);
		deathFadeScreen.CrossFadeAlpha (0f, 0f, true);
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
		if (canMove) {
			movement = new Vector2 (moveHor, moveVer);
		}

		rigidBody.velocity = movement * moveSpeed;

		//Attack
		if (Input.GetAxisRaw ("Attack") == 1) {
			attackScript.attack (attackTime, this.gameObject);
		}

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
			//playerHealth = Mathf.Min (playerHealth + 1, maxHealth);
		}
	}

	//Coinage Scripts

	public void addCoin(){
		coins += 1;
		Debug.Log ("added coin");
	}

	public void addEssence(){
		essences += 1;
		Debug.Log ("added essence");
	}

	//Squadage Scripts

	public void addSquadMember(GameObject squadMember){
		squad.Add (squadMember);
		Debug.Log (squad);
	}

	public void setCanMove(bool boolie){
		canMove = boolie;
	}
}
