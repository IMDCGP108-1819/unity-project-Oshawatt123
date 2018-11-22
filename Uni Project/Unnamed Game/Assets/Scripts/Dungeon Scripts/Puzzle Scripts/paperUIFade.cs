using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class paperUIFade : MonoBehaviour {

	private Image paperImage;

	// Use this for initialization
	void Start () {
		paperImage = this.GetComponent<Image> ();
		StartCoroutine (fadeAndDestroy ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator fadeAndDestroy(){
		paperImage.CrossFadeAlpha (0f, 4f, true);
		yield return new WaitForSeconds (4f);
		Destroy (this.gameObject);
	}
}
