using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePickup : MonoBehaviour {

	private LifeManager lifeSystem;
	public AudioSource lifeUpSfx;
	public GameObject lifeUpEffect;

	// Use this for initialization
	void Start () {
		lifeSystem = FindObjectOfType<LifeManager> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		
		if (other.name == "Player") {
			lifeSystem.GiveLife ();
			lifeUpSfx.Play ();
			Destroy (gameObject);
			Instantiate (lifeUpEffect, transform.position, transform.rotation);
				
		}
	}

}
