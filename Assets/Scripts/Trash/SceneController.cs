/*
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	//private int nextDoor;
	//public static SceneController instance;
	//[SerializeField] private string[] scenes;
	[SerializeField] private int function;
	//[SerializeField] private GameObject player;

	void Awake(){
	}

	public void DoTheThing(){
		switch(function){
		case 0:
			QuitGame ();
			break;
		default:
			LoadScene (function - 1);
			break;
		}
	}

	 void LoadScene(int scene){
		Debug.Log ("loading");
		mySceneManager.instance.LoadScene (scene);
	}

	 void QuitGame(){
		Application.Quit ();
	}
}
*/