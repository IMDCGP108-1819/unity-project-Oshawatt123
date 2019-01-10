using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIfade : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//makes the UI invisible and non-interactable
		this.GetComponent<CanvasGroup> ().alpha = 0f;
		this.GetComponent<CanvasGroup> ().interactable = false;
	}
}
