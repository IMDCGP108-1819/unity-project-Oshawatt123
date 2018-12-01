using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestInteract : MonoBehaviour {

	private Collider2D[] nearbyObjects_Chest;

	private Vector2 pos;

	public GameObject gizmoMarker;

	public float xDetect;
	public float yDetect;
	private Vector2 detectSize;

	public LayerMask playerLayer;

	public Animator chestAnim;

	private bool lootSpawned = false;

	// Use this for initialization
	void Start () {
		detectSize = new Vector2 (xDetect, yDetect);
		pos = new Vector2 (this.transform.position.x, this.transform.position.y);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E)) {
			Collider2D[] nearbyObjects_Chest = Physics2D.OverlapBoxAll (pos, detectSize, 0, playerLayer);
			Debug.Log (nearbyObjects_Chest.Length);
			if(nearbyObjects_Chest.Length > 0){
				Debug.Log ("Opening chest");
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
		if (!lootSpawned) {
			Debug.Log ("Spawning Loot");
			lootSpawned = true;
		}
		// shower coins everywhere
		// create coins
		// 
		// create own "particle system" for it
	}
}
