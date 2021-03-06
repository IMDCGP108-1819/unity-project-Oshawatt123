using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paperInteract : MonoBehaviour {

	private Collider2D[] nearbyObjects;

	private Vector2 pos;

	public GameObject gizmoMarker;

	private Canvas canvas;
	public GameObject paperUI;
	private GameObject inst_paperUI; // instance of the paperUI so it can be cleaned up

	public float xDetect;
	public float yDetect;
	private Vector2 detectSize;

	public LayerMask playerLayer;

	// Use this for initialization
	void Start () {
		// find the object and setup a interaction space for the object
		canvas = FindObjectOfType<Canvas> ();
		detectSize = new Vector2 (xDetect, yDetect);
		pos = new Vector2 (this.transform.position.x, this.transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E)) {
			// get all objects within the interaction space on the player's layer
			Collider2D[] nearbyObjects = Physics2D.OverlapBoxAll (pos, detectSize, 0, playerLayer);
			//Debug.Log (nearbyObjects.Length); <- for debugging
			if(nearbyObjects.Length > 0){
				//Debug.Log ("Reading paper"); <- more debugging
				inst_paperUI = Instantiate (paperUI, canvas.transform); // instantiate a paperUI object and keep it's instance
				inst_paperUI.transform.SetParent (canvas.transform); // set the parent of the instance to the Canvas
				// I set the parent to the canvas because this is a UI element and should be on the canvas
			}
		}
	}
	// Draw a 'gizmo' so I can see the interaction space in the scene view
	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (gizmoMarker.transform.position, new Vector3 (xDetect, yDetect, 0));
	}
}