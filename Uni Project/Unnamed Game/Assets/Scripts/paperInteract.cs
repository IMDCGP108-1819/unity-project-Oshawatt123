using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paperInteract : MonoBehaviour {

	private GameObject player;
	private Vector2 pos;

	public GameObject gizmoMarker;

	private Canvas canvas;
	public GameObject paperUI;
	private GameObject inst_paperUI;

	public float xDetect;
	public float yDetect;
	private Vector2 detectSize;

	public LayerMask playerLayer;

	// Use this for initialization
	void Start () {
		canvas = FindObjectOfType<Canvas> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		detectSize = new Vector2 (xDetect, yDetect);
		pos = new Vector2 (this.transform.position.x, this.transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E)) {
			Collider2D[] nearbyObjects = Physics2D.OverlapBoxAll (pos, detectSize, 0);
			Debug.Log (nearbyObjects.Length);
			if(nearbyObjects.Length > 0){
				inst_paperUI = Instantiate (paperUI, canvas.transform);
				inst_paperUI.transform.SetParent (canvas.transform);
				// create UI object that fades
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (gizmoMarker.transform.position, new Vector3 (xDetect, yDetect, 0));
	}
}