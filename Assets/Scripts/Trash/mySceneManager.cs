/*
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class mySceneManager : MonoBehaviour {

	public static mySceneManager instance;

	[SerializeField] private string[] scenes;
	private Camera camera;

	// Use this for initialization
	void Awake () {
		if(instance == null){
			instance = this;
		}else{
			Object.Destroy (gameObject);
		}
	}

	void Start(){
		camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Debug.Log ("clicked");
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);	
			if (Physics2D.Raycast (ray.origin, ray.direction, 11f)) {
				Debug.Log ("hit something");
				RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 11f);
				GameObject objectHit = hit.collider.gameObject;
				if (objectHit.tag == "Button") {
					Debug.Log ("hit a button");
					objectHit.GetComponent<SceneController> ().DoTheThing ();
				}
			}
		}
	}

	public void LoadScene(int scene){
		Debug.Log ("loading");
		SceneManager.LoadScene (scenes [scene]);
	}
}
*/