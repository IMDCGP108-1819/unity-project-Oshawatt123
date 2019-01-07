using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIfade : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<CanvasGroup> ().alpha = 0f;
		this.GetComponent<CanvasGroup> ().interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
