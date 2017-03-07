using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField] private GameObject bullet;
	[SerializeField] private bool fire;
	[SerializeField] private AudioClip explosion, shoot;

	private float fireRate;
	private float fireTime;
	private float bulletSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > fireTime + fireRate){
			Fire ();
			fireTime = Time.time;
		}
	}

	void OnTriggerEnter2D(Collider2D trigger){
		//Debug.Log ("Hit something");
		if(trigger.tag == "Right" || trigger.CompareTag("Left")){
			Debug.Log (trigger.tag);
			PlayerManager.instance.ChangeDirections (trigger.tag);
		}else if(trigger.CompareTag("Deadly")){
			PlayerManager.instance.InvaderDeath ();
			EnemyAI.instance.RemoveBullet (gameObject);
			AudioManager.instance.PlayClip (explosion);
			Object.Destroy (gameObject);
		}else if(trigger.CompareTag("Goal")){
			SceneController.instance.LoadScene (2);
		}
	}

	void Fire(){
		GameObject newBullet = Instantiate (bullet, transform.position, bullet.transform.rotation) as GameObject;
		newBullet.GetComponent<BulletController>().SetSpeed (bulletSpeed);
		EnemyAI.instance.AddBullet (newBullet);
		AudioManager.instance.PlayClip (shoot);
	}

	public void SetStuff(float _fireRate, float _bulletSpeed){
		fireRate = _fireRate;
		fireTime = -Random.Range (0, fireRate) + Time.time;
		Debug.Log (fireTime);
		bulletSpeed = _bulletSpeed;

	}

}
