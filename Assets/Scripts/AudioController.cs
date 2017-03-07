using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	public static AudioController instance;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
			Object.DontDestroyOnLoad (gameObject);
		} else {
			Object.Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
