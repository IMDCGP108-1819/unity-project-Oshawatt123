using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour {

	public void loadLevel(string sceneToLoad){
		SceneManager.LoadSceneAsync(sceneToLoad);
	}

	public void exitGame(){
		Application.Quit ();
	}
}
