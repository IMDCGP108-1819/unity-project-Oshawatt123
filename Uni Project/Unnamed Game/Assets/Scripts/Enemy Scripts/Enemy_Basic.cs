using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Basic : MonoBehaviour {

	// Attached components
	public Transform hitBoxTransform;
	public Rigidbody2D rigidBody;

	// Animation Variables
	public Animator enemyAnim;

	public GameObject blood;

	//Combat Variables
	public float maxHealth;
	private float health;
	public float knockbackMod;
	public float stopDistance;
	private Vector2Int roomID;
	public float attackTime;

	public playerAttack attackScript;

	// Movement Variables
	private GameObject player;
	private PlayerMovement playerScript;
	private Vector2 playerPos;
	private Vector2 toPlayer;
	private bool awareOfPlayer = false;

	private bool dazed = false;

	private float mag;
	private Vector2 toPlayerUnit;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		player = GameObject.FindGameObjectWithTag ("Player");
		playerScript = player.GetComponent<PlayerMovement> ();
		roomID = new Vector2Int ((Mathf.FloorToInt(this.transform.position.x) / 10) - 20, (Mathf.FloorToInt(this.transform.position.y) / 10) - 19);
	}
	
	// Update is called once per frame
	void Update () {
		roomID = new Vector2Int ((Mathf.FloorToInt(this.transform.position.x) / 10) - 20, (Mathf.FloorToInt(this.transform.position.y) / 10) - 19);
		if (awareOfPlayer && !dazed) {
			playerPos = new Vector2 (player.transform.position.x, player.transform.position.y);
			toPlayer = playerPos - new Vector2 (this.transform.position.x, this.transform.position.y);

			// vv future stuff vv
			// Conditional movement
			// once near enough to the player to attack, then attack!!!
			// asleep enemies will be woken on contact (movement by player collision)
			// ^^ future stuff ^^

			mag = Mathf.Sqrt (toPlayer.x * toPlayer.x + toPlayer.y * toPlayer.y); // magnitude of the vector
			toPlayerUnit = toPlayer / mag;
			if (health <= (maxHealth / 10)) {
				rigidBody.velocity = -toPlayerUnit;
			} else if (Mathf.Abs (toPlayer.x) > stopDistance || Mathf.Abs (toPlayer.y) > stopDistance) {
				rigidBody.velocity = toPlayerUnit;
			} else {
				attackScript.attack(attackTime, this.gameObject);
			}

			hitBoxTransform.localPosition = new Vector3 (0, 0, 0);
		} else {
			if (playerScript.roomID == roomID) {
				awareOfPlayer = true;
			}
		}
		if (health <= 0) {
			//DIEEEEEEEE (death animation)
			Debug.Log ("Ded m8");
			Destroy (this.gameObject);
		}

		attackTime -= Time.deltaTime;
	}

	public void takeDamage(float damage){
		Instantiate (blood, new Vector3(this.transform.position.x, this.transform.position.y, -3f), Quaternion.identity);
		dazed = true;
		rigidBody.AddForce (-toPlayer * knockbackMod, ForceMode2D.Impulse);
		health -= damage;
		StartCoroutine(Undaze ());
	}

	private IEnumerator Undaze() {
		yield return new WaitForSeconds (0.5f);
		dazed = false;
	}

	public void endAttackAnim(){
		enemyAnim.SetBool("isAttacking", false);
	}

	public Vector2Int getRoomID(){
		return roomID;
	}
}