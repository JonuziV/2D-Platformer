using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string startLevel;
	public string levelSelect;
	public int playerLives;
	public int playerHealth;

	public void NewGame(){
		PlayerPrefs.SetInt ("PlayerLives", playerLives );
		PlayerPrefs.SetInt("CurrentScore", 0);
		PlayerPrefs.SetInt ("PlayerHealth", playerHealth);
		PlayerPrefs.SetInt ("PlayerMaxHealth", playerHealth);
		SceneManager.LoadScene (startLevel);
	}

	public void LevelSelect(){
		PlayerPrefs.SetInt ("PlayerLives", playerLives );
		PlayerPrefs.SetInt("CurrentScore", 0);
		PlayerPrefs.SetInt ("PlayerHealth", playerHealth);
		PlayerPrefs.SetInt ("PlayerMaxHealth", playerHealth);
		SceneManager.LoadScene (levelSelect);
	}

	public void QuitGame(){
		Application.Quit ();
		Debug.Log ("Quit Game!");
	}
}
