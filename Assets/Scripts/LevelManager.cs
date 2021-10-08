using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {


	public GameObject currentCheckpoint;

	private PlayerController player;

	public GameObject deathParticle;
	public GameObject respawnParticle;

	public float respawnDelay;

	private CameraController camera;

	public int pointPenaltyOnDeath;

	private float gravityStore;

	public HealthManager healthManager;

	private Animator anim;
	public GameObject sword;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
		camera = FindObjectOfType<CameraController> ();
		healthManager = FindObjectOfType<HealthManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RespawnPlayer(){
		StartCoroutine ("RespawnPlayerCo");
	}

	public IEnumerator RespawnPlayerCo()
	{
		Instantiate (deathParticle, player.transform.position, player.transform.rotation);
		player.transform.parent = null;
		player.enabled = false; 
		sword.SetActive (false);
		player.GetComponent<Renderer>().enabled  = false;
		camera.isFollowing = false;
		//gravityStore = player.GetComponent<Rigidbody2D> ().gravityScale;
		//player.GetComponent<Rigidbody2D> ().gravityScale = 0f;
		//player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		ScoreManager.AddPoints (-pointPenaltyOnDeath);
		Debug.Log ("Player Respawned!");
		player.gameObject.SetActive(false);
		yield return new WaitForSeconds (respawnDelay);
		player.gameObject.SetActive(true);
		player.transform.position = currentCheckpoint.transform.position;
		player.knonckbackCount = 0;
		//player.GetComponent<Rigidbody2D> ().gravityScale = gravityStore;
		//player.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
		player.enabled = true; 
		player.GetComponent<Renderer>().enabled  = true;
		healthManager.FullHealth ();
		healthManager.isDead = false;
		camera.isFollowing = true;
		Instantiate (respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
	}
}
