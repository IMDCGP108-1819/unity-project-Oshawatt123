using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour {

	// Timing Variables
	private float attackTime;
	public float attackStartTime;

	// Collision Variables
	public Transform attackPos;
	public float attackRangeX;
	public float attackRangeY;
	public LayerMask mobLayer;

	// Damage variables
	public float damage;
	
	// Update is called once per frame
	void Update () {
		if (attackTime <= 0) {
			if (Input.GetAxisRaw ("Attack") == 1) {
				attackTime = attackStartTime;
				Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll (attackPos.position, new Vector2 (attackRangeX, attackRangeY), 0, mobLayer);
				for (int i = 0; i < enemiesToDamage.Length; i++) {
					enemiesToDamage [i].GetComponentInParent<Enemy_Basic> ().takeDamage (damage);
					Debug.Log ("Trying :3");
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
