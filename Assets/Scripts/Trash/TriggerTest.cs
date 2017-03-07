using UnityEngine;
using System.Collections;

public class TriggerTest : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			//Debug.Log ("clicked");
			EventManager.TriggerEvent ("someListener");
		}
	}
}
