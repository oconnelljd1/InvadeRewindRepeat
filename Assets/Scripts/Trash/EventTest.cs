using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EventTest : MonoBehaviour {

	private UnityAction someListener;

	// Use this for initialization
	void Awake () {
		someListener = new UnityAction (SomeFunction);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable(){
		EventManager.StartListening ("someListener", someListener);
	}

	void OnDisable(){
		EventManager.StopListening ("someListener", someListener);
	}

	void SomeFunction(){
		Debug.Log ("SomeFuntion was called");
	}

}
