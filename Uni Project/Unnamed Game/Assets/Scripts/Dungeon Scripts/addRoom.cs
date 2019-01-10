using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addRoom : MonoBehaviour {

	private roomTemplates templates;

	void Start(){
		// add the room to a list of rooms in the roomTemplates object
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<roomTemplates>();
		templates.rooms.Add (this.gameObject);
	}
}