using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour {

	public GameObject MainObject;
	public GameObject HTPObject;

	private CanvasGroup MainGroup;
	private CanvasGroup HTPGroup;

	void Start(){
		MainGroup = MainObject.GetComponent<CanvasGroup> ();
		HTPGroup = HTPObject.GetComponent<CanvasGroup> ();
		MainScreen ();
	}

	public void loadLevel(string sceneToLoad){
		SceneManager.LoadSceneAsync(sceneToLoad);
	}

	public void HTPScreen(){
		MainGroup.alpha = 0f;
		MainGroup.interactable = false;
		HTPGroup.alpha = 1f;
		HTPGroup.interactable = true;
	}

	public void MainScreen(){
		MainGroup.alpha = 1f;
		MainGroup.interactable = true;
		HTPGroup.alpha = 0f;
		HTPGroup.interactable = false;
	}

	public void exitGame(){
		Application.Quit ();
	}

}
