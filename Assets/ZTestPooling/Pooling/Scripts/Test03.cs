using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test03 : MonoBehaviour {
	public Transform spawnPoint;
	public float force = 100f;
	public float speed = 2f;
	public float turnSpeed = 90f;
	string bullet = "Capsule";
	 float nextFireTime;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			bullet = "Capsule";
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			bullet = "Cube";
		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			bullet = "Sphere";
		}

		if (Input.GetMouseButton (0) && Time.time > nextFireTime ) {
			nextFireTime = Time.time + Constant.FIRE_TIME;
			//Rigidbody _rb = listRB [idx];
			//_rb = Instantiate (_rb, spawnPoint.position, spawnPoint.rotation) as Rigidbody;
			//_rb.AddForce (spawnPoint.forward * force);
			//_rb.velocity = spawnPoint.forward * 10f;
			GameObject _go = PoolManager.ins.Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
			_go.GetComponent<Rigidbody>().velocity = spawnPoint.forward * 50f;
		}

		float _h = Input.GetAxisRaw ("Horizontal");
		float _v = Input.GetAxisRaw ("Vertical");

		transform.Translate (_v * Vector3.forward * speed * Time.deltaTime);
		transform.Rotate (_h * Vector3.up * turnSpeed * Time.deltaTime);
	}	
}
