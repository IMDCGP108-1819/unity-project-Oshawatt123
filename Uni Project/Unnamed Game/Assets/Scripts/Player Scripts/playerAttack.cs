using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour {
	/// Attack Script
	/// Is NOT the scripts for just player attacking, doubles up for the enemies too
	/// </summary>

	// Layer Variables
	public LayerMask mobLayer;
	private LayerMask playerLayer;
	private LayerMask enemyLayer;


	// Timing Variables
	public float attackStartTime;
	private bool notAlreadyAttacked = true;

	// Collision Variables
	public Transform attackPos;
	public float attackRangeX;
	public float attackRangeY;

	// Damage variables
	public float damage;

	// Animation variables
	public Animator playerAnim;

	void Start(){
		playerLayer = LayerMask.NameToLayer ("playerHitBox");
		enemyLayer = LayerMask.NameToLayer ("enemyHitBox");
	}

	// Update is called once per frame
	public void attack(float attackTime, GameObject attacker) {
		if (attackTime <= 0) {
			notAlreadyAttacked = true;
			Debug.Log ("Try 1");
			if (notAlreadyAttacked == true) {
				playerAnim.SetBool ("isAttacking", true);
				if (attacker.CompareTag("Player")){
					attacker.GetComponent<PlayerMovement>().attackTime = attackStartTime;
				}
				notAlreadyAttacked = false;
				Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll (attackPos.position, new Vector2 (attackRangeX, attackRangeY), 0, mobLayer);
				for (int i = 0; i < enemiesToDamage.Length; i++) {
					if (mobLayer == playerLayer) {
						Debug.Log ("Try player");
						enemiesToDamage [i].GetComponentInParent<Enemy_Basic> ().takeDamage (damage);
					} else if (mobLayer == enemyLayer) {
						Debug.Log ("Try enemy");
						enemiesToDamage [i].GetComponentInParent<PlayerMovement>().takeDamage (damage);
					}
				}
			}
		}
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (attackPos.position, new Vector3 (attackRangeX, attackRangeY, 0));
	}
}
