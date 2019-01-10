using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ladderScript : MonoBehaviour {

	public float detectX;
	public float detectY;
	private Vector2 detectSize;
	public GameObject entryRoom;

	public LayerMask playerLayer;

	private Vector3 floorLocation = new Vector3(200, 600, 0);

	void Start(){
		detectSize = new Vector2 (detectX, detectY);
	}
	
	public void nextScene(){

		// nextScene() used to change to the next scene, but now
		// new floors work differently, although the name persists

		//SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		GameObject.Find ("roomTemplates").GetComponent<roomTemplates> ().newFloor();
		floorLocation = GameObject.Find ("roomTemplates").GetComponent<roomTemplates> ().getFloorLocation(GameObject.Find("roomTemplates").GetComponent<roomTemplates>().floor);
		GameObject.Find ("Anvil").GetComponent<anvilScript> ().openAnvilUI ();
		GameObject.Find ("cage(Clone)").GetComponent<squadSpawn> ().openSquadUI ();
		// Resetting and moving objects so they can work again on the new floor with no issues
		Instantiate(entryRoom, floorLocation, Quaternion.identity);
		GameObject.Find ("Playerparent").GetComponentInChildren<PlayerMovement> ().newFloor (floorLocation);
		GameObject.Find ("Playerparent").GetComponentInChildren<squadControl> ().newFloor (floorLocation);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.E)) {
			Collider2D[] nearbyObjects_Ladder = Physics2D.OverlapBoxAll (this.transform.position, detectSize, 0, playerLayer);
			if (nearbyObjects_Ladder.Length > 0) {
				nextScene();
			}
		}
	}

	// gizmo for visualisation of interaction space
	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (this.transform.position, new Vector3 (detectX, detectY, 0));
	}
}
