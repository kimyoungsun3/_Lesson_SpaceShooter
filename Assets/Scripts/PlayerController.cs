using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {
	//@@@@ SpawnPoint
	public enum STATS_FIRE_MODE { One, Two, Three, Five};
	public STATS_FIRE_MODE fireMode;
	Queue<STATS_FIRE_MODE> fireQueue = new Queue<STATS_FIRE_MODE> ();


	Transform trans;
	public Boundary boundary;
	//float y;
	//Vector3 move, pos;
	//public Vector3 boundX, boundZ;
	//public float moveSpeed = 10f;
	//public float tilt = 5f;

	//발사...
	//public float NEXT_TIME = 0.25f;
	//float nextTime;
	//@@@@ TouchMove
	Plane plane = new Plane(Vector3.up, Vector3.zero);				//touchmove
	float distacne = 0;
	Vector3 offset;
	public Transform spawnPoint, spawnPointl, spawnPointr, spawnPointl2, spawnPointr2;
	public float spawnBetweenTime = 0.5f;
	float spawnTime;

	void Start(){
		SoundManager.ins.Play ("BGM", true);
		trans = transform;
		//y = trans.position.y;
		health = HEALTH_MAX;

		//int[] ii = { 1, 2, 3 };
		//Utility.ShufflArray<int> (ii);
		//for (int i = 0, iMax = ii.Length; i < iMax; i++)
		//	Debug.Log (ii [i]);

		//Vector3[] ff = { Vector3.one, Vector3.one*2, Vector3.one*3 };
		//Utility.ShufflArray<Vector3> (ff);
		//for (int i = 0, iMax = ff.Length; i < iMax; i++)
		//	Debug.Log (ff [i]);

		//enum -> queue에 넣고 선회하기
		Array _arr = Enum.GetValues (typeof(STATS_FIRE_MODE));
		for (int i = 0; i < _arr.Length; i++) {
			fireQueue.Enqueue ((STATS_FIRE_MODE)_arr.GetValue (i));	
		}
	}

	void Update(){
		/*
		//이동...
		float h = Input.GetAxis("Horizontal");	//좌,우이동.
		float v = Input.GetAxis("Vertical");		//상,하이동.
		move.Set(h, y, v); 
		move = move.normalized;			//대각선이동의 값을 동일하게 하기위해.
		trans.Translate(move * moveSpeed * Time.deltaTime, Space.World);

		//경계라인 검사하기....
		pos = trans.position;
		pos.x = Mathf.Clamp(pos.x, boundX.x, boundX.y);
		pos.z = Mathf.Clamp(pos.z, boundZ.x, boundZ.y);
		trans.position = pos;

		//발사하기.
		if (Time.time > nextTime) {
			nextTime = Time.time + NEXT_TIME;
			//Transform _t = Instantiate (prefabBullet, spawnPoint.position, spawnPoint.rotation) as Transform;
			PoolManager.ins.Instantiate("PlayerBullet", 
				spawnPoint.position, 
				spawnPoint.rotation);
		}

		//틸트 회전.
		trans.rotation = Quaternion.Euler(0, 0, -h * tilt);

		GetComponent<Rigidbody> ().velocity = Vector3.forward * moveSpeed;
		*/

		//@@@@ TouchMove
		Move ();

		//@@@@ SpawnPoint
		Shoot ();
	}


	//@@@@ TouchMove
	void Move(){
		if (Input.GetMouseButtonDown (0)) 
		{
			Ray _ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (plane.Raycast (_ray, out distacne)) 
			{
				offset = _ray.GetPoint (distacne) - transform.position;
			}
		}
		else if(Input.GetMouseButton(0))
		{
			Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(plane.Raycast(_ray, out distacne)){
				transform.position = _ray.GetPoint(distacne) - offset;
				transform.position = new Vector3 (
					Mathf.Clamp (transform.position.x, boundary.xMin, boundary.xMax),
					0,
					Mathf.Clamp (transform.position.z, boundary.zMin, boundary.zMax)
				);
			}
		}

		//무기변경.
		if (Input.GetMouseButtonDown (1)) {
			fireMode = fireQueue.Dequeue();
			fireQueue.Enqueue (fireMode);
		}
	}

	//@@@@ SpawnPoint
	void Shoot(){
		//if (Input.GetMouseButton (0) && Time.time > spawnTime) {
		if ( Time.time > spawnTime) {
			spawnTime = Time.time + spawnBetweenTime;

			switch (fireMode) {
			case STATS_FIRE_MODE.One:
				PoolManager.ins.Instantiate ("PlayerBullet", spawnPoint.position, Quaternion.identity);
				break;
			case STATS_FIRE_MODE.Two:
				PoolManager.ins.Instantiate ("PlayerBullet", spawnPointl.position, Quaternion.identity);
				PoolManager.ins.Instantiate ("PlayerBullet", spawnPointr.position, Quaternion.identity);
				break;
			case STATS_FIRE_MODE.Three:
				PoolManager.ins.Instantiate ("PlayerBullet", spawnPoint.position, Quaternion.identity);
				PoolManager.ins.Instantiate ("PlayerBullet", spawnPointl.position, Quaternion.identity);
				PoolManager.ins.Instantiate ("PlayerBullet", spawnPointr.position, Quaternion.identity);
				break;
			case STATS_FIRE_MODE.Five:
				PoolManager.ins.Instantiate ("PlayerBullet", spawnPoint.position, Quaternion.identity);
				PoolManager.ins.Instantiate ("PlayerBullet", spawnPointl.position, Quaternion.identity);
				PoolManager.ins.Instantiate ("PlayerBullet", spawnPointr.position, Quaternion.identity);
				PoolManager.ins.Instantiate ("PlayerBullet", spawnPointr2.position, Quaternion.identity);
				PoolManager.ins.Instantiate ("PlayerBullet", spawnPointl2.position, Quaternion.identity);
				break;
			}

			SoundManager.ins.Play ("SHOOT", false);
		}
	}
	public float HEALTH_MAX  = 10f;
	float health;
	bool bDie = false;
	//public GameObject prefabExplosion;
	public void HitDamage(float _damage, Vector3 _hitPoint){
		health -= _damage;
		//Debug.Log ("health:" + health + " _damage:" + _damage);

		if (!bDie && health <= 0) {
			Die ();
		}
	}

	void Die(){
		bDie = true;
		//Debug.Log("#### die -> Particle");
		PoolManager.ins.Instantiate("explosion_player", trans.position, trans.rotation);
		SpawerManager.ins.GameOver ();
		OnDestroy();
	}

	void OnDestroy(){
		gameObject.SetActive (false);
	}
}


[System.Serializable]
public class Boundary{
	public float xMin, xMax, zMin, zMax;
}
