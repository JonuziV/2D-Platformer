using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

	public static int playerHealth;
	public int maxPlayerHealth;

	Text text;

	private LevelManager levelManager;

	public bool isDead;

	private LifeManager lifeSystem;

	private TimeManager theTime;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		//playerHealth = maxPlayerHealth;
		playerHealth = PlayerPrefs.GetInt("PlayerHealth");
		theTime = FindObjectOfType<TimeManager> ();
		levelManager = FindObjectOfType<LevelManager> ();
		lifeSystem = FindObjectOfType<LifeManager> ();
		isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerHealth <= 0 && !isDead) {
			playerHealth = 0;
			levelManager.RespawnPlayer ();
			lifeSystem.TakeLife ();
			isDead = true;
			theTime.ResetTime ();
		}

		text.text = "" + playerHealth;
	}

	public static void HurtPlayer(int damageToGive){
		playerHealth -= damageToGive;
		PlayerPrefs.SetInt ("PlayerHealth", playerHealth);
	}

	public void FullHealth(){
		playerHealth = PlayerPrefs.GetInt("PlayerMaxHealth");
		PlayerPrefs.SetInt ("PlayerHealth", playerHealth);

	}

	public void KillPlayer(){
		playerHealth = 0;
	}
}
