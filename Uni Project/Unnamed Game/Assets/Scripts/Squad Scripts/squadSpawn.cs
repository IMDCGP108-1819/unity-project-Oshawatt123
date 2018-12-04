using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squadSpawn : MonoBehaviour {

	public GameObject gizmoMarker;

	public GameObject[] squadMembers;

	private Vector2 detectSize;
	public float detectSizeX;
	public float detectSizeY;
	private Vector2 Pos;

	public LayerMask playerLayer;

	void Start(){
		detectSize = new Vector2 (detectSizeX, detectSizeY);
		Pos = new Vector2 (this.transform.position.x, this.transform.position.y);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.E)) {
			Collider2D[] nearbyObjects_Cage = Physics2D.OverlapBoxAll (Pos, detectSize, 0, playerLayer);
			if (nearbyObjects_Cage.Length > 0) {
				Debug.Log ("Player getting squad member");
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (gizmoMarker.transform.position, new Vector3 (detectSizeX, detectSizeY, 0));
	}

}