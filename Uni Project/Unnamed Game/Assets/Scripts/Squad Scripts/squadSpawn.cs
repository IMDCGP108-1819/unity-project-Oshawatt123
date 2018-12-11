using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squadSpawn : MonoBehaviour {

	public GameObject gizmoMarker;

	public GameObject[] squadMembers;

	public GameObject player;

	public GameObject SquadRecruitPopUp;
	private bool addingMember = false;
	private bool addedMember = false;

	private Vector2 detectSize;
	public float detectSizeX;
	public float detectSizeY;
	private Vector2 Pos;

	public LayerMask playerLayer;

	void Start(){
		SquadRecruitPopUp = GameObject.FindWithTag ("squadUIPopUp");
		SquadRecruitPopUp.SetActive (false);
		/*canvasGroups = FindObjectsOfType<CanvasGroup> ();
		for (int i = 0; i < canvasGroups.Length; i++) {
			if(canvasGroups[i].CompareTag("squadUIPopUp")){           // this did not find the correct object, so chris did that ^^
				SquadRecruitPopUp = canvasGroups[i].gameObject;
			}
		}*/
		player = GameObject.FindGameObjectWithTag ("Player");
		detectSize = new Vector2 (detectSizeX, detectSizeY);
		Pos = new Vector2 (this.transform.position.x, this.transform.position.y);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.E)) {
			if (!addedMember) {
				Collider2D[] nearbyObjects_Cage = Physics2D.OverlapBoxAll (Pos, detectSize, 0, playerLayer);
				if (nearbyObjects_Cage.Length > 0) {
					Debug.Log ("Player getting squad member");
					if (!addingMember) {
						SquadRecruitOverlay ();
					}
				}
			}
		}
	}

	/*void OnGUI(){
		if (addingMember) {
			// create the UI
			if (GUI.Button (new Rect (100, 100, 100, 100), "Yes")) {
				Debug.Log ("dfbhBIGNFSDFGB");
				addSquadMember ();
				addingMember = false;
			}
			if (GUI.Button (new Rect (10, 10, 10, 10), "My Other Button")) {
				Debug.Log ("feferg343t45t545467567");
				addingMember = false;
			}
		}
	}*/

	/*public void BtnAddSquadMember(){
		addSquadMember();
	}*/

	public void addSquadMember(){
		GameObject tempSquadMember = Instantiate (squadMembers [Random.Range (0, squadMembers.Length - 1)], player.transform.position, Quaternion.identity) as GameObject;
		player.GetComponent<squadControl> ().addSquadMember (tempSquadMember);
		SquadRecruitPopUp.SetActive (false);
		addingMember = false;
		addedMember = true;
	}

	private void SquadRecruitOverlay(){
		addingMember = true;
		SquadRecruitPopUp.SetActive (true);
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (gizmoMarker.transform.position, new Vector3 (detectSizeX, detectSizeY, 0));
	}

}