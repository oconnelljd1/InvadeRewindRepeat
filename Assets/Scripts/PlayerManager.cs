using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

	public static PlayerManager instance;

	private int direction = 1;
	private float delay = 1;
	private List<GameObject> invaders = new List<GameObject>();
	private List<GameObject> shields = new List<GameObject> ();
	private List<GameObject> bullets = new List<GameObject>();
	[SerializeField]private GameObject invader;
	[SerializeField]private GameObject shield;
	[SerializeField]private Text guiMoney;

	private int money = 1000000;
	private int columns = 10;

	private int shipValue, rows;
	private float bulletSpeed, fireRate ,moveSpeedH ,moveSpeedV;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else {
			Object.Destroy (gameObject);
		}
	}

	// Use this for initialization
	public void StartGameplay () {
		Debug.Log ("starting gameplay");
		SpawnInvaders ();
		SpawnShields ();
		StartCoroutine ("Move");
		guiMoney.text = "" + money;
	}

	public void SpawnShields(){
		shields = new List<GameObject> ();
		for(int i = -1; i < 2; i ++){
			GameObject newShield = Object.Instantiate (shield, new Vector3 (i * 4, -3, 0), shield.transform.rotation) as GameObject;
			shields.Add (newShield);
		}
	}

	public void ClearShields(){
		foreach(GameObject s in shields){
			Object.Destroy (s);
		}
	}

	public void SpawnInvaders (){
		for(int i = 0; i < rows; i++){
			for(int o = 0; o < columns; o ++){
				GameObject newInvader = Object.Instantiate (invader, new Vector3(o - 4.5f, i, 0), invader.transform.rotation)as GameObject;
				newInvader.GetComponent<PlayerController> ().SetStuff (fireRate, bulletSpeed);
				invaders.Add (newInvader);
			}
		}
		EnemyAI.instance.SetInvaders (invaders);
		//EnemyAI.instance.StartIt();
	}

	private IEnumerator Move(){
		bool yes = true;
		while(yes){
			yield return new WaitForSeconds(delay);
			foreach(GameObject ship in invaders){
				if(ship != null){
					ship.transform.position += (Vector3.right * direction * moveSpeedH);
				}
			}
		}
	}

	public void ChangeDirections(string _tag){
		Debug.Log ("Changing");
		if (_tag == "Right") {
			direction = -1;
		} else if(_tag == "Left"){
			direction = 1;
		}

		Debug.Log ("Direction: " + direction);
		StopAllCoroutines ();
		StartCoroutine (MoveDown ());
	}

	private IEnumerator MoveDown(){
		Debug.Log ("MoveDown");
		yield return new WaitForSeconds(delay);
		foreach(GameObject ship in invaders){
			if(ship != null){
				ship.transform.position += (Vector3.up * -moveSpeedV);
			}
		}
		StartCoroutine ("Move");
	}

	public void InvaderDeath(){
		money += shipValue;
		guiMoney.text = "" + money;
	}
		
	public float GetBulletSpeed(){
		return bulletSpeed;
	}

	public int GetMoney(){
		return money;
	}

	public void ChangeMoney(int _change){
		money += _change;
		guiMoney.text = "" + money;
	}

	public void SetStats(int _shipValue, int _rows, float _bulletSpeed, float _fireRate, float _moveSpeedH, float _moveSpeedV){
		shipValue = _shipValue;
		rows = _rows;
		bulletSpeed = _bulletSpeed;
		fireRate = _fireRate;
		moveSpeedH = _moveSpeedH;
		moveSpeedV = _moveSpeedV;
	}

	public void AddBullet(GameObject _bullet){
		bullets.Add(_bullet);
	}

	public void UseNuke(){
		foreach(GameObject b in bullets){
			if(b != null){
				Object.Destroy (b);
			}
		}
		bullets.Clear ();
	}

	public void UseShield(){
		StartCoroutine ("myUseShield");
	}

	private IEnumerator myUseShield(){
		foreach (GameObject s in shields){
			s.SetActive (false);
		}
		yield return new WaitForSeconds (5);
		foreach (GameObject s in shields){
			s.SetActive (true);
		}
	}

}
