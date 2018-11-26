using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestInteract : MonoBehaviour {

	private Collider2D[] nearbyObjects;

	private Vector2 pos;

	public GameObject gizmoMarker;

	public float xDetect;
	public float yDetect;
	private Vector2 detectSize;

	public LayerMask playerLayer;

	public Animator chestAnim;

	// Use this for initialization
	void Start () {
		detectSize = new Vector2 (xDetect, yDetect);
		pos = new Vector2 (this.transform.position.x, this.transform.position.y);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E)) {
			Collider2D[] nearbyObjects = Physics2D.OverlapBoxAll (pos, detectSize, 0);
			Debug.Log (nearbyObjects.Length);
			if(nearbyObjects.Length > 0){
				chestAnim.SetTrigger ("openChest");
				//set trigger on chest animation
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (gizmoMarker.transform.position, new Vector3 (xDetect, yDetect, 0));
	}

	public void spawnLoot(){
		Debug.Log ("Spawning Loot");
		// shower coins everywhere
		// create coins
		// 
		// create particle system for it
	}
}
