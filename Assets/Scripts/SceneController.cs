using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	//private int nextDoor;
	public static SceneController instance;
	[SerializeField] private string[] scenes;
	//[SerializeField] private GameObject player;

	void Awake(){
		if(instance != null){
			Object.Destroy (gameObject);
		}else{
			instance = this;
			//DontDestroyOnLoad (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadScene(int scene){
		Debug.Log ("loading");
		SceneManager.LoadScene (scenes [scene]);
	}

	public void QuitGame(){
		Application.Quit ();
	}
	/*
	public void SetNextDoor(int NextDoor){
		nextDoor = NextDoor;
	}

	public void PositionPlayer (Vector3 temp){
		player.transform.position = temp;
	}
	*/
}
