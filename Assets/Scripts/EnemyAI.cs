using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {

	public static EnemyAI instance;

	[SerializeField] private GameObject bullet;
	[SerializeField] private LayerMask invaderLayermask, shieldLayerMask;
	[SerializeField] private Vector2[] shieldPoints;

	[SerializeField] private AudioClip explosion, shoot;

	private Animator anim;

	private bool moved = false;
	private bool canShoot = true;
	private bool won = false;

	private int health = 0;

	private float moveSpeed = 10;
	private float shotTime = 0;
	private float shotDelay = 0.2f;

	private List<GameObject> invaders;
	private List<GameObject> bullets = new List<GameObject>();
	private List<GameObject> unSafeSpots;

	private Vector3 target;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else {
			Object.Destroy (gameObject);
		}
	}

	private void Start(){
		anim = GetComponent<Animator> ();
	}

	// Use this for initialization
	public void StartGameplay () {
		won = false;
		health = 0;
	}
	
	// Update is called once per frame
	void Update () {
		moved = false;
		CheckBullets ();
		if(!moved){
			//CheckBarriers ();
		}
		if(!moved){
			CheckMove ();
		}
		CheckShoot ();
		CheckWin ();
	}

	void OnTriggerEnter2D(Collider2D trigger){
		if(trigger.CompareTag("Pointy")){
			anim.SetTrigger ("Hit");
			Debug.Log ("OUCH");
			health++;
			PlayerManager.instance.ChangeMoney (health * health * 1000);
			StartCoroutine ("MakeInvincible");
			AudioManager.instance.PlayClip (explosion);
			if( health >= 3){
				Debug.Log ("DEAD DEAD DEAD DEAD DEAD");
				SceneController.instance.LoadScene (2);
			}
		}
	}

	private IEnumerator MakeInvincible(){
		GetComponent<Collider2D> ().enabled = false;
		yield return new WaitForSeconds (5);
		GetComponent<Collider2D> ().enabled = true;
	}

	private float FindDistance(Vector3 _final, Vector3 _initial){
		return _final.x - _initial.x;
	}

	private int GetPositiveNegative(Vector3 _final, Vector3 _initial){
		float distance = FindDistance (_final, _initial);
		if(distance > 0){
			return -1;
		}
		return 1;
	}

	private void MoveTowards(int _posNeg){
		moved = true;
		transform.Translate(Vector3.right * _posNeg * moveSpeed * Time.deltaTime, Space.World);
	}

	void CheckBullets(){
		//Debug.Log ("bullets");
		float bulletSpeed = PlayerManager.instance.GetBulletSpeed ();
		float minDistance = (bulletSpeed * Time.deltaTime) + (moveSpeed * Time.deltaTime) + 1.2f;
		unSafeSpots = new List<GameObject> ();
		foreach(GameObject shot in bullets){
			Vector3 displacement = transform.position - shot.transform.position;
			float currentDistance = displacement.sqrMagnitude;
			if(currentDistance < minDistance * minDistance){
				unSafeSpots.Add(shot);
			}
		}
		//Debug.Log (unSafeSpots.Count);
		if(unSafeSpots.Count > 0){
			CheckForSafeAreas (unSafeSpots);
		}
	}

	void CheckForSafeAreas(List<GameObject> _closeBullets){
		List<Vector3> safeSpots = new List<Vector3> ();
		foreach (GameObject shot in _closeBullets) {
			Vector3 point1 = shot.transform.position;
			point1.x += 1f + (moveSpeed * Time.deltaTime);
			Vector3 point2 = shot.transform.position;
			point2.x += 1f + (moveSpeed * Time.deltaTime);
			safeSpots.Add (point1);
			safeSpots.Add (point2);
		}
		List<Vector3> realSafeSpots = new List<Vector3> ();
		foreach(Vector3 pos in safeSpots){
			bool safe = true;
			foreach (GameObject bullet in unSafeSpots) {
				if (Mathf.Abs (FindDistance (pos, bullet.transform.position)) < 1.6f) {
					safe = false;
					break;
				}
				if(safe){
					realSafeSpots.Add (pos);
				}
			}
		}
		Vector3 closestSafeSpot = new Vector3 (0,0,0);
		float closestDistance = 100;
		foreach(Vector3 pos in realSafeSpots){
			if(Mathf.Abs(FindDistance(pos, transform.position)) < closestDistance){
				closestSafeSpot = pos;
				closestDistance = Mathf.Abs (FindDistance (pos, transform.position));
			}
		}
		Vector3 destination = transform.position;
		destination.x = closestSafeSpot.x;
		//Debug.Log (destination);
		transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
		moved = true;
	}

	private void CheckBarriers(){
		//Debug.Log ("Barriers");
		for(int i = 0; i < shieldPoints.Length; i ++){
			if(transform.position.x > shieldPoints[i].x && transform.position.x < shieldPoints[i].y){
				float neg = Mathf.Abs (transform.position.x - shieldPoints[i].x);
				float pos = Mathf.Abs (transform.position.x - shieldPoints[i].y);
				Vector3 destination = transform.position;
				if (neg < pos) {
						destination.x = shieldPoints[i].x;
				} else {
					destination.x = shieldPoints[i].y;
				}
			transform.position = Vector3.MoveTowards (transform.position, destination, moveSpeed * Time.deltaTime);
				moved = true;
			}
			break;
		}
	}

	private void CheckMove(){
		//Debug.Log ("Move");
		GameObject target = FindTarget ();
		if(target != null){
			Vector3 destination = transform.position;
			destination.x = target.transform.position.x;
			transform.position = Vector3.MoveTowards (transform.position, destination, moveSpeed * Time.deltaTime);
		}
	}

	private GameObject FindTarget(){
		float shortestDistanceX = 100;
		GameObject target = null;
		foreach(GameObject invader in invaders){
			if(invader != null){
				bool shielded = false;
				for(int i = 0; i < shieldPoints.Length; i ++){
					if(invader.transform.position.x > shieldPoints[i].x && invader.transform.position.x < shieldPoints[i].y){
						shielded = true;
						break;
					}
				}
				if (shielded) {
					continue;
				} else {
					foreach(GameObject pos in unSafeSpots){
						if(invader.transform.position.x > pos.transform.position.x - 0.5f && invader.transform.position.x < pos.transform.position.x + 0.5f){
							shielded = true;
							break;
						}
					}
					if (shielded) {
						continue;
					} else {
						float distanceX = Mathf.Abs (FindDistance(invader.transform.position, transform.position));
						if (distanceX < shortestDistanceX) {
							shortestDistanceX = distanceX;
							target = invader;
						} else if (distanceX == shortestDistanceX) {
							float newDistanceY = Mathf.Abs (invader.transform.position.y - transform.position.y);
							if (newDistanceY < Mathf.Abs (target.transform.position.y - transform.position.y)) {
								target = invader;
							}
						}
					}
				}
			}
		}
		return target;
	}

	private void CheckShoot(){
		Debug.Log ("ChecKShooting");
		if(canShoot){
			Debug.Log ("canshoot");
			if(Time.time > shotTime + shotDelay){
				Debug.Log ("in time");
				if(Physics2D.Raycast(transform.position + Vector3.up, Vector3.up, 10, invaderLayermask)){
					Debug.Log ("see something");
					RaycastHit2D hit= Physics2D.Raycast (transform.position + Vector3.up, Vector3.up, 10, invaderLayermask);
					//Debug.DrawRay (transform.position, Vector3.up * 10, Color.blue);
					//Debug.Log (hit.collider.gameObject.tag);
					if(hit.collider.CompareTag("Player")){
						Debug.Log ("it's a player");
						bool shielded = false;
						for(int i = 0; i < shieldPoints.Length; i ++){
							if(transform.position.x > shieldPoints[i].x && transform.position.x < shieldPoints[i].y){
								shielded = true;
								break;
							}
						}
						if (!shielded) {
							Debug.Log ("not shielded");
							shotTime = Time.time;
							//Debug.Log ("shoting");
							GameObject newShot = Object.Instantiate (bullet, transform.position, bullet.transform.rotation) as GameObject;
							PlayerManager.instance.AddBullet (newShot);
							AudioManager.instance.PlayClip (shoot);
						}
					}
				}
			}
		}
	}

	private void CheckWin(){
		if (!won) {
			bool alive = false;
			foreach (GameObject ship in invaders) {
				if (ship != null) {
					alive = true;
					break;
				}
			}
			if (!alive) {
				//Debug.Log ("winner!");
				PlayerManager.instance.ClearShields ();
				foreach (GameObject b in bullets) {
					if (b != null) {
						Object.Destroy (b);
					}
				}
				bullets.Clear ();
				UpgradeManager.instance.StartStore ();
				won = true;
			}
		}
	}

	public void AddBullet(GameObject _bullet){
		bullets.Add (_bullet);
	}

	public void RemoveBullet(GameObject _bullet){
		bullets.Remove (_bullet);
	}

	public void SetInvaders(List<GameObject> _invaders){
		invaders = _invaders;
	}

	public void RemoveInvader(GameObject _invader){
		invaders.Remove (_invader);
	}

	public List<GameObject> GetBullets(){
		return bullets;
	}

	public void UseTime (){
		StartCoroutine ("myUseTime");
	}

	private IEnumerator myUseTime(){
		moveSpeed = 1;
		yield return new WaitForSeconds (5);
		moveSpeed = 10;
	}

	public void UseGuns(){
		StartCoroutine ("myUseGuns");
	}

	private IEnumerator myUseGuns(){
		moveSpeed = 1;
		yield return new WaitForSeconds (5);
		moveSpeed = 5;
	}

}

/*
 * 
				*/
