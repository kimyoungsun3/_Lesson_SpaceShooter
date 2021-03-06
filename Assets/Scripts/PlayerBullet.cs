﻿using System.Collections;
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
	public LayerMask enemyMask;
	public LayerMask asteroidMask;
	//public GameObject prefabExplosion;

	// Use this for initialization
	void Start () {
		trans = transform;	
	}
	float spawnTime;
	public float SPAWN_ALIVE_TIME = 2f;
	void OnEnable(){
		spawnTime = Time.time + SPAWN_ALIVE_TIME;
	}

	public float radius = 0.06f;
	#if UNITY_EDITOR
	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (trans.position, radius);
		Gizmos.DrawWireSphere (trans.position + trans.forward * distance, radius);
		Gizmos.DrawRay (trans.position, trans.forward * distance);
	}
	#endif

	// Update is called once per frame
	void Update () {
		ray.origin 		= trans.position;
		ray.direction 	= trans.forward;
		distance = speed * Time.deltaTime + 0.2f;	//스킨값 추가...
		//Debug.DrawRay (ray.origin, ray.direction * distance, Color.blue);
		//if(Physics.Raycast(ray, out hit, distance, mask, QueryTriggerInteraction.Collide)){
		if(Physics.SphereCast(ray, radius, out hit, distance, mask, QueryTriggerInteraction.Collide)){
			HitCheck ();
			return;
		}

		trans.Translate (Vector3.forward * speed * Time.deltaTime);
		if (Time.time > spawnTime) {
			OnDestroy ();
		}
	}

	void HitCheck(){
		int _hitLayer = hit.collider.gameObject.layer;
		if (CheckMask (_hitLayer, boundaryMask.value) > 0) {
			//벽과 충돌(X)....> 파티클...
			//Debug.Log ("Bullet 경계와 충돌...");
		} else if (CheckMask (_hitLayer, enemyMask.value) > 0 
			|| CheckMask (_hitLayer, asteroidMask.value) > 0) {
			//Debug.Log ("Bullet 행성과 충돌...");
			PoolManager.ins.Instantiate("explosion_hit2", hit.point, Quaternion.identity);
			EnemyMaster _scp = hit.collider.GetComponent<EnemyMaster>();
			if (_scp != null) {
				_scp.HitDamage (1, hit.point);
			}
		}
		OnDestroy ();
	}

	void OnDestroy(){
		//Destroy (gameObject);
		gameObject.SetActive(false);
	}

	int CheckMask(int _layer, int _mask){
		//return ((int)Mathf.Pow (2, _layer) & _mask);
		return ((2 << _layer - 1) & _mask);
	}


	/*

	void HitCheck(){
		int _hitLayer = hit.collider.gameObject.layer;
		if (CheckMask (_hitLayer, boundaryMask.value) > 0) {
			//벽과 충돌(X)....> 파티클...
			Debug.Log ("경계와 충돌...");
		} else if (CheckMask (_hitLayer, enemyMask.value) > 0) {
			Debug.Log ("적과 충돌...");
		} else if (CheckMask (_hitLayer, AsteroidMask.value) > 0) {
		}
		OnDestroy ();
	}
	*/
}
