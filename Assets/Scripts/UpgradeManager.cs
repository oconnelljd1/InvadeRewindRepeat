using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour {

	public static UpgradeManager instance;

	[SerializeField]private Text shipValueText, daysText;

	[SerializeField]private GameObject upgradeCanvas, guiCanvas;
	[SerializeField]private Text moneyText;

	private int rowsUpgradeCost = 1000;
	private int bulletSpeedCost = 1500;
	private int fireRateCost = 1800;
	private int moveSpeedHCost = 2000;
	private int moveSpeedVCost = 2000;

	private int days;

	private int shipValue = 100;
	private int rows = 1;
	private float bulletSpeed = 1f;
	private float fireRate = 20f;
	private float moveSpeedH = 0.1f;
	private float moveSpeedV = 0.2f;

	private int rowsUpgradeIndex, bulletSpeedUpgradeIndex, fireRateUpgradeIndex, moveSpeedHUpgradeIndex, moveSpeedVUpgradeIndex;

	[SerializeField]private Text rowsCostText, bulletSpeedCostText, fireRateCostText, moveSpeedHCostText, moveSpeedVCostText; 

	void Awake(){
		if(instance == null){
			instance = this;
		}else{
			Object.Destroy (gameObject);
		}
	}

	void SetShipValue(){
		shipValueText.text = "Ship Value = " + shipValue;
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("starting");
		StartGameplay ();
		rowsCostText.text = "" + rowsUpgradeCost;
		bulletSpeedCostText.text = "" + bulletSpeedCost;
		fireRateCostText.text = "" + fireRateCost;
		moveSpeedHCostText.text = "" + moveSpeedHCost;
		moveSpeedVCostText.text = "" + moveSpeedVCost;
		SetShipValue ();
	}
	
	// Update is called once per frame
	void UpdateDaysText () {
		days++;
		daysText.text = "DAYS SPENT: " + days;
	}

	public void updateMoneyText(){
		moneyText.text = "" + PlayerManager.instance.GetMoney ();
	}

	public void StartGameplay(){
		Time.timeScale = 1;
		PlayerManager.instance.SetStats (shipValue, rows, bulletSpeed, fireRate, moveSpeedH, moveSpeedV);
		PlayerManager.instance.StartGameplay ();
		upgradeCanvas.SetActive (false);
		guiCanvas.SetActive (true);
		SabotageManager.instance.StartGamePlay ();
		EnemyAI.instance.StartGameplay ();
	}

	public void StartStore(){
		Debug.Log ("StartScore");
		Time.timeScale = 0;
		upgradeCanvas.SetActive (true);
		guiCanvas.SetActive (false);
		updateMoneyText ();
		UpdateDaysText ();
	}

	public void UpgradeRows(){
		if(PlayerManager.instance.GetMoney() >= rowsUpgradeCost){
			if(rowsUpgradeIndex < 4){
				rowsUpgradeIndex++;
				PlayerManager.instance.ChangeMoney (-rowsUpgradeCost);
				updateMoneyText ();
				rows++;
				rowsUpgradeCost = rows * rows * rows * 1000;
				if (rowsUpgradeIndex == 4) {
					rowsCostText.text = "Sold Out" + "";
				} else {
					rowsCostText.text = "" + rowsUpgradeCost;
				}
			}
		}
	}

	public void UpgradeBulletSpeed(){
		if (PlayerManager.instance.GetMoney () >= bulletSpeedCost) {
			if(bulletSpeedUpgradeIndex < 5){
				bulletSpeedUpgradeIndex++;
				PlayerManager.instance.ChangeMoney (-bulletSpeedCost);
				updateMoneyText ();
				shipValue += bulletSpeedUpgradeIndex * 10;
				SetShipValue ();
				bulletSpeed++;
				bulletSpeedCost = Mathf.FloorToInt(Mathf.Pow (1.5f, bulletSpeed) * 1000);
				bulletSpeedCostText.text = "" + bulletSpeedCost;
			}
			if(bulletSpeedUpgradeIndex == 5){
				bulletSpeedCostText.text = "Sold Out";
			}
		}
	}

	public void UpgradeFireRate(){
		if (PlayerManager.instance.GetMoney () >= fireRateCost) {
			if(fireRateUpgradeIndex < 5){
				fireRateUpgradeIndex++;
				PlayerManager.instance.ChangeMoney (-fireRateCost);
				updateMoneyText ();
				shipValue += fireRateUpgradeIndex * 10;
				SetShipValue ();
				fireRate -= 3;
				fireRateCost = Mathf.FloorToInt((((20 - fireRate) / 2) + ((20 - fireRate) / 3)) * 1000);
				fireRateCostText.text = "" + fireRateCost;
			}
			if(fireRateUpgradeIndex == 5){
				fireRateCostText.text = "Sold Out";
			}
		}
	}

	public void UpgradeMoveSpeedH(){
		if (PlayerManager.instance.GetMoney () >= moveSpeedHCost) {
			if(moveSpeedHUpgradeIndex < 5){
				moveSpeedHUpgradeIndex++;
				PlayerManager.instance.ChangeMoney (-moveSpeedHCost);
				updateMoneyText ();
				shipValue += moveSpeedHUpgradeIndex * 10;
				SetShipValue ();
				moveSpeedH += 0.1f;
				moveSpeedHCost = Mathf.FloorToInt(moveSpeedHCost * 1.5f);
				moveSpeedHCostText.text = "" + moveSpeedHCost;
			}
			if(moveSpeedHUpgradeIndex == 5){
				moveSpeedHCostText.text = "Sold Out";
			}
		}
	}

	public void UpgradeMoveSpeedV(){
		if (PlayerManager.instance.GetMoney () >= moveSpeedVCost) {
			if(moveSpeedVUpgradeIndex < 5){
				moveSpeedVUpgradeIndex++;
				PlayerManager.instance.ChangeMoney (-moveSpeedVCost);
				updateMoneyText ();
				shipValue += moveSpeedVUpgradeIndex * 10;
				SetShipValue ();
				moveSpeedV += 0.2f;
				moveSpeedVCost += moveSpeedVCost;
				moveSpeedVCostText.text = "" + moveSpeedVCost;
			}
			if(moveSpeedVUpgradeIndex == 5){
				moveSpeedVCostText.text = "Sold Out";
			}
		}
	}

}
