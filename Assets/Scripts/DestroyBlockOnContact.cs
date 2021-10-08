using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlockOnContact : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Ground") {
			Destroy (other.gameObject);
		}
	}
}
