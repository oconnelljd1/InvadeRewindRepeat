using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;
	[SerializeField]private AudioSource source;

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

	public void PlayClip(AudioClip _clip){
		source.Stop ();
		source.clip = _clip;
		source.Play ();
	}
}
