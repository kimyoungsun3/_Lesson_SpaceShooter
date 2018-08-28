using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {
	public float speed = 20f;
	Transform trans;
	Ray ray;
	RaycastHit hit;
	float distance;
	public LayerMask mask;
	public LayerMask boundaryMask;

	// Use this for initialization
	void Start () {
		trans = transform;	
	}
	
	// Update is called once per frame
	void Update () {
		ray.origin = trans.position;
		ray.direction = trans.forward;
		distance = speed * Time.deltaTime;
		if(Physics.Raycast(ray, out hit, distance, mask, QueryTriggerInteraction.Collide)){
			HitCheck ();
			return;
		}

		trans.Translate (Vector3.forward * speed * Time.deltaTime);
	}

	void HitCheck(){
		if (hit.collider.gameObject.layer != Mathf.Log (boundaryMask.value, 2)) {
			//벽과 충돌(X)....> 파티클...
		}
		OnDestroy ();
	}

	void OnDestroy(){
		Destroy (gameObject);
	}
}
