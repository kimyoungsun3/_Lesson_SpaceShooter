using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	Transform trans;
	float y;
	Vector3 move, pos;
	public Vector3 boundX, boundZ;
	public float moveSpeed = 10f;
	public float tilt = 5f;

	//발사...
	public float NEXT_TIME = 0.25f;
	float nextTime;
	//public Transform prefabBullet;
	public Transform spawnPoint;

	void Start(){
		trans = transform;
		y = trans.position.y;
		health = HEALTH_MAX;

		int[] ii = { 1, 2, 3 };
		Utility.ShufflArray<int> (ii);
		for (int i = 0, iMax = ii.Length; i < iMax; i++)
			Debug.Log (ii [i]);

		Vector3[] ff = { Vector3.one, Vector3.one*2, Vector3.one*3 };
		Utility.ShufflArray<Vector3> (ff);
		for (int i = 0, iMax = ff.Length; i < iMax; i++)
			Debug.Log (ff [i]);
	}

	void Update(){
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

	}

	public float HEALTH_MAX  = 10f;
	float health;
	bool bDie = false;
	//public GameObject prefabExplosion;
	public void HitDamage(float _damage, Vector3 _hitPoint){
		health -= _damage;
		//Debug.Log ("#### hit particle, sound");

		if (!bDie && health <= 0) {
			Die ();
		}
	}

	void Die(){
		bDie = true;
		//Debug.Log("#### die -> Particle");
		PoolManager.ins.Instantiate("explosion_player", trans.position, trans.rotation);
		OnDestroy();
	}

	void OnDestroy(){
		Destroy (gameObject);
	}
}
