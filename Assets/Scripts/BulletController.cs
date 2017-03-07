using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	[SerializeField]private float moveSpeed;
	[SerializeField]private string tag;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (-transform.up * moveSpeed * Time.deltaTime, Space.World);
	}

	void OnTriggerEnter2D(Collider2D trigger){
		if(trigger.CompareTag(tag) || trigger.CompareTag("Shield") || trigger.CompareTag("End")){
			if(tag == "Enemy"){
				EnemyAI.instance.RemoveBullet (gameObject);
			}
			Object.Destroy (gameObject);
		}
	}

	public void SetSpeed(float _moveSpeed){
		moveSpeed = _moveSpeed;
	}
}
