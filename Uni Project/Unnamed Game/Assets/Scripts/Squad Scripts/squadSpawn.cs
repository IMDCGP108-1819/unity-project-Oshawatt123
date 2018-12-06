using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squadSpawn : MonoBehaviour {

	public GameObject gizmoMarker;

	public GameObject[] squadMembers;

	private GameObject player;

	public GameObject SquadRecruitPopUp;
	private Canvas canvas;

	private Vector2 detectSize;
	public float detectSizeX;
	public float detectSizeY;
	private Vector2 Pos;

	public LayerMask playerLayer;

	void Start(){
		canvas = FindObjectOfType<Canvas> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		detectSize = new Vector2 (detectSizeX, detectSizeY);
		Pos = new Vector2 (this.transform.position.x, this.transform.position.y);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.E)) {
			Collider2D[] nearbyObjects_Cage = Physics2D.OverlapBoxAll (Pos, detectSize, 0, playerLayer);
			if (nearbyObjects_Cage.Length > 0) {
				Debug.Log ("Player getting squad member");
				GameObject tempSquadMember = Instantiate (squadMembers [Random.Range (0, squadMembers.Length - 1)], player.transform.position, Quaternion.identity) as GameObject;
				player.GetComponent<PlayerMovement> ().addSquadMember (tempSquadMember);
				//SquadRecruitOverlay ();
			}
		}
	}

	private void SquadRecruitOverlay(){
		GameObject UIOverlay = Instantiate (SquadRecruitPopUp, canvas.transform);
		UIOverlay.transform.SetParent (canvas.transform);
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (gizmoMarker.transform.position, new Vector3 (detectSizeX, detectSizeY, 0));
	}

}