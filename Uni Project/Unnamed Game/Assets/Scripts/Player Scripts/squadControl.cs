using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class squadControl : MonoBehaviour {

	private List<GameObject> squadMembers = new List<GameObject>();

	public void addSquadMember(GameObject squadMember){
		squadMembers.Add (squadMember);
	}

	void Start(){
		Debug.Log ("oh my!");
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode){
		Debug.Log ("New scene tadaaa");
		foreach (GameObject squadDude in squadMembers){
			Instantiate (squadDude.gameObject, this.transform, false);
		}
	}

	public void newFloor(Vector3 floorLocation){
		foreach (GameObject squadMember in squadMembers) {
			squadMember.transform.position = floorLocation;
		}
	}

}