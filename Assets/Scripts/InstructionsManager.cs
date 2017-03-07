using UnityEngine;
using System.Collections;

public class InstructionsManager : MonoBehaviour {

	[SerializeField]private GameObject[] pages;
	private int index = 0;

	// Use this for initialization
	void Start () {
		SetActive ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetActive(){
		foreach(GameObject page in pages){
			page.SetActive (false);
		}
		pages [index].SetActive (true);
		index++;
	}

}
