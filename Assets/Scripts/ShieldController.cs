using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour {

	[SerializeField]private bool pos;
	[SerializeField] private AudioClip explosion;

	// Use this for initialization
	void Start () {
		transform.localEulerAngles = new Vector3 (0, 0, Random.Range(0,4) * 90);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D trigger){
		if(trigger.CompareTag("Deadly") || trigger.CompareTag("Pointy")){
			AudioManager.instance.PlayClip (explosion);
			Object.Destroy (gameObject);
		}
	}

	public int GetPosNeg(){
		if(pos){
			return 1;
		}else{
			return -1;
		}
	}
}
