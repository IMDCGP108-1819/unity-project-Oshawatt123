using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour {

	// Timing Variables
	private float attackTime;
	public float attackStartTime;
	private bool notAlreadyAttacked = true;

	// Collision Variables
	public Transform attackPos;
	public float attackRangeX;
	public float attackRangeY;
	public LayerMask mobLayer;

	// Damage variables
	public float damage;

	// Animation variables
	public Animator playerAnim;
	
	// Update is called once per frame
	void Update () {
		if (attackTime <= 0) {
			notAlreadyAttacked = true;
			if (Input.GetAxisRaw ("Attack") == 1 && notAlreadyAttacked == true) {
				playerAnim.SetBool ("isAttacking", true);
				attackTime = attackStartTime;
				notAlreadyAttacked = false;
				Debug.Log ("Really trying");
				Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll (attackPos.position, new Vector2 (attackRangeX, attackRangeY), 0, mobLayer);
				for (int i = 0; i < enemiesToDamage.Length; i++) {
					enemiesToDamage [i].GetComponentInParent<Enemy_Basic> ().takeDamage (damage);
					Debug.Log ("Trying :3");
					Debug.Log (i);
				}
			}
		}else{
			attackTime -= Time.deltaTime;
		}
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (attackPos.position, new Vector3 (attackRangeX, attackRangeY, 0));
	}
}
