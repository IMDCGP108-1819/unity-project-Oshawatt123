using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class anvilScript : MonoBehaviour {

	public float detectX;
	public float detectY;
	private Vector2 detectSize;

	public LayerMask playerLayer;

	public GameObject AnvilUI;

	// Use this for initialization
	void Start () {
		detectSize = new Vector2 (detectX, detectY);
		AnvilUI = GameObject.Find ("AnvilPopUp");
		AnvilUI.SetActive (false);
	}
	
	void Update(){
		if (Input.GetKeyDown (KeyCode.E)) {
			Collider2D[] nearbyObjects_Anvil = Physics2D.OverlapBoxAll (this.transform.position, detectSize, 0, playerLayer);
			if (nearbyObjects_Anvil.Length > 0) {
				openAnvilUI ();
			}
		}
	}

	public void openAnvilUI(){
		Debug.Log ("open anvil UI");
		AnvilUI.SetActive (true);
		GameObject.Find ("EssenceCost").GetComponent<TextMeshProUGUI> ().text = GameObject.Find ("Player").GetComponent<PlayerMovement> ().getEssenceCost ().ToString();
		GameObject.Find ("CoinCost").GetComponent<TextMeshProUGUI> ().text = GameObject.Find ("Player").GetComponent<PlayerMovement> ().getCoinCost ().ToString();
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireCube (this.transform.position, new Vector3 (detectX, detectY, 0));
	}
}
