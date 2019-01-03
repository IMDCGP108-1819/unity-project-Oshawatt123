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
		//SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		GameObject.Find ("roomTemplates").GetComponent<roomTemplates> ().newFloor();
		GameObject.Find ("Playerparent").GetComponentInChildren<PlayerMovement> ().newFloor (floorLocation);
		Instantiate(entryRoom, floorLocation, Quaternion.identity);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.E)) {
			Collider2D[] nearbyObjects_Ladder = Physics2D.OverlapBoxAll (this.transform.position, detectSize, 0, playerLayer);
			if (nearbyObjects_Ladder.Length > 0) {
				nextScene();
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (this.transform.position, new Vector3 (detectX, detectY, 0));
	}
}
