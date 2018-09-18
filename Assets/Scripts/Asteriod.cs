using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteriod : MonoBehaviour {
	Rigidbody rb;
	Transform trans;
	public float tumble = 10f;
	public float moveSpeed = 3f;
	void Start () {
		trans 	= transform;
		rb 		= GetComponent<Rigidbody> ();

		//임의의 방향으로 회전...
		rb.angularVelocity = Random.insideUnitSphere * tumble;
		health = HEALTH_MAX;
	}
	
	// Update is called once per frame
	void Update () {
		//아래 방향으로 이동하기..(회전축이 변화해서 World공간을 기준으로 이동)
		trans.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);		
	}

	void OnTriggerEnter(Collider _col){		
		if (_col.CompareTag ("Boundary")) {
			//아래 경계라인에 충돌..
			Debug.Log ("Asteriod 경계와 충돌...");
			OnDestroy();
		} else if (_col.CompareTag ("Player")) {
			//플레이어와 충돌..
			Debug.Log ("Asteriod Player와 충돌...");
			PoolManager.ins.Instantiate("explosion_asteroid", trans.position, trans.rotation);

			PlayerController _scpPlayer = _col.GetComponent<PlayerController> ();
			if (_scpPlayer != null) {
				_scpPlayer.HitDamage (1, trans.position);
			}
			OnDestroy ();
		}
	}

	public float HEALTH_MAX  = 1f;
	float health;
	bool bDie = false;
	public void HitDamage(float _damage, Vector3 _hitPoint){
		
		health -= _damage;
		PoolManager.ins.Instantiate("explosion_hit", _hitPoint, Quaternion.identity);

		if (!bDie && health <= 0) {
			Die ();
		}
	}

	void Die(){
		PoolManager.ins.Instantiate("explosion_asteroid", trans.position, trans.rotation);
		bDie = true;
		OnDestroy();
	}

	void OnDestroy(){
		gameObject.SetActive(false);
	}
}
