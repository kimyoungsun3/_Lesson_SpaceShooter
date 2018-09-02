using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet2 : MonoBehaviour {
	public float speed = 20f;

	void Start () {
		GetComponent<Rigidbody> ().velocity = transform.forward * speed;	
	}

	void OnTriggerEnter(Collider _col){
		if (_col.CompareTag ("Boundary")) {
			Destroy (gameObject);
		}
	}
}
