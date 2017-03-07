using UnityEngine;
using System.Collections;

public class ButtonAudios : MonoBehaviour {

	public static ButtonAudios instance;

	[SerializeField] private AudioClip select;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void PlayButton(){
		AudioManager.instance.PlayClip (select);
	}

}
