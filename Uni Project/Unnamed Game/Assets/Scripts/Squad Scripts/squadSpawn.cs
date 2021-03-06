﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class squadSpawn : MonoBehaviour {
	
	public GameObject gizmoMarker;

	public GameObject[] squadMembers;

	public GameObject player;

	public GameObject SquadRecruitPopUp;
	public bool addingMember = false;
	private bool addedMember = false;

	private Vector2 detectSize;
	public float detectSizeX;
	public float detectSizeY;
	private Vector2 Pos;

	public LayerMask playerLayer;

	private float[] squadMemberStats = new float[3];

	void Start(){
		// generate created squad member stats
		squadMemberStats [0] = Random.Range(1, 4); // detect range
		squadMemberStats [1] = Random.Range(3, 7); // attack damage
		squadMemberStats [2] = Random.Range(20, 150); // health

		SquadRecruitPopUp = GameObject.FindWithTag ("squadUIPopUp");
		SquadRecruitPopUp.GetComponent<CanvasGroup> ().alpha = 0f;
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

	public float[] getSquadMemberStats(){
		return squadMemberStats;
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

	public void openSquadUI(){
		SquadRecruitPopUp.SetActive(true);
		SquadRecruitPopUp.GetComponent<CanvasGroup> ().alpha = 0f;
		SquadRecruitPopUp.GetComponent<CanvasGroup> ().interactable = false;
	}

	private void SquadRecruitOverlay(){
		SquadRecruitPopUp.SetActive (true);
		addingMember = true;
		SquadRecruitPopUp.GetComponent<CanvasGroup> ().alpha = 1f;
		SquadRecruitPopUp.GetComponent<CanvasGroup> ().interactable = true;
		GameObject.Find ("IntText").GetComponent<TextMeshProUGUI> ().text = squadMemberStats [0].ToString();
		GameObject.Find ("StrText").GetComponent<TextMeshProUGUI> ().text = squadMemberStats [1].ToString();
		GameObject.Find ("DefText").GetComponent<TextMeshProUGUI> ().text = squadMemberStats [2].ToString();
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (gizmoMarker.transform.position, new Vector3 (detectSizeX, detectSizeY, 0));
	}

}