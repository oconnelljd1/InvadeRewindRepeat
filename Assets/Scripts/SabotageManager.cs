using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SabotageManager : MonoBehaviour {

	public static SabotageManager instance;

	private bool timeUnlocked, nukeUnlocked, shieldUnlocked, gunsUnlocked;
	private bool timeUsed, nukeUsed, shieldUsed, gunsUsed;

	[SerializeField]private GameObject time, nuke, shield, guns;
	[SerializeField]private Text timeText, nukeText, shieldText, gunsText;

	private int timeCost = 3000;
	private int nukeCost = 3000;
	private int shieldCost = 5000;
	private int gunsCost = 7500;

	void Awake(){
		if(instance == null){
			instance = this;
		}else{
			Object.Destroy (gameObject);
		}
	}

	// Use this for initialization
	public void StartGamePlay () {
		Debug.Log ("sabotage started");
		if(timeUnlocked){
			time.GetComponent<Image>().color = Color.white;
		}else{
			time.GetComponent<Image>().color = Color.gray;
		}
		if(nukeUnlocked){
			nuke.GetComponent<Image>().color = Color.white;
		}else{
			nuke.GetComponent<Image>().color = Color.gray;
		}
		if(shieldUnlocked){
			shield.GetComponent<Image>().color = Color.white;
		}else{
			shield.GetComponent<Image>().color = Color.gray;
		}
		if(gunsUnlocked){
			guns.GetComponent<Image>().color = Color.white;
		}else{
			guns.GetComponent<Image>().color = Color.gray;
		}
		timeUsed = false;
		nukeUsed = false;
		shieldUsed = false;
		gunsUsed = false;
	}

	public void UseTime(){
		if(timeUnlocked && !timeUsed){
			timeUsed = true;
			EnemyAI.instance.UseTime ();
			time.GetComponent<Image>().color = Color.gray;
		}
	}

	public void UseNuke(){
		if(nukeUnlocked && !nukeUsed){
			PlayerManager.instance.UseNuke ();
			nukeUsed = true;
			nuke.GetComponent<Image>().color = Color.gray;
		}
	}

	public void UseShield(){
		if(shieldUnlocked && !shieldUsed){
			PlayerManager.instance.UseShield ();
			shieldUsed = true;
			shield.GetComponent<Image>().color = Color.gray;
		}
	}

	public void UseGuns(){
		if(gunsUnlocked && !gunsUsed){
			gunsUsed = true;
			EnemyAI.instance.UseGuns ();
			guns.GetComponent<Image>().color = Color.gray;
		}
	}

	public void BuyTime(){
		if(PlayerManager.instance.GetMoney() >= timeCost){
			if(!timeUnlocked){
				PlayerManager.instance.ChangeMoney (-timeCost);
				timeUnlocked = true;
				time.GetComponent<Image>().color = Color.gray;
				timeText.text = "Sold Out";
				UpgradeManager.instance.updateMoneyText ();
			}
		}
	}

	public void BuyNuke(){
		if(PlayerManager.instance.GetMoney() >= nukeCost){
			if(!nukeUnlocked){
				PlayerManager.instance.ChangeMoney (-nukeCost);
				nukeUnlocked = true;
				nuke.GetComponent<Image>().color = Color.gray;
				nukeText.text = "Sold Out";
				UpgradeManager.instance.updateMoneyText ();
			}
		}
	}

	public void BuyShield(){
		if(PlayerManager.instance.GetMoney() >=shieldCost){
			if(!shieldUnlocked){
				PlayerManager.instance.ChangeMoney (-shieldCost);
				shieldUnlocked = true;
				shield.GetComponent<Image>().color = Color.gray;
				shieldText.text = "Sold Out";
				UpgradeManager.instance.updateMoneyText ();
			}
		}
	}

	public void BuyGuns(){
		if(PlayerManager.instance.GetMoney() >= gunsCost){
			if(!gunsUnlocked){
				PlayerManager.instance.ChangeMoney (-gunsCost);
				gunsUnlocked = true;
				guns.GetComponent<Image>().color = Color.gray;
				gunsText.text = "Sold Out";
				UpgradeManager.instance.updateMoneyText ();
			}
		}
	}
}
