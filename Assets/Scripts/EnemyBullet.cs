using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
	public float moveSpeed = 4f;
	Rigidbody rb;
	Transform trans;
	public float tumble = 10f;
	public GameObject prefabHitExplosion;
	public GameObject prefabDieExplosion;

	void Start () {
		trans = transform;
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		//아래 방향으로 이동하기..(회전축이 변화해서 World공간을 기준으로 이동)
		trans.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);		
	}

	void OnTriggerEnter(Collider _col){		
		if (_col.CompareTag ("Boundary")) {
			//아래 경계라인에 충돌..
			OnDestroy();
		} else if (_col.CompareTag ("Player")) {
			//플레이어와 충돌..
			Instantiate(prefabDieExplosion, trans.position, trans.rotation);

			PlayerController _scpPlayer = _col.GetComponent<PlayerController> ();
			_scpPlayer.HitDamage(1, trans.position);
			OnDestroy ();
		}
	}

	void OnDestroy(){
		Destroy (gameObject);
	}
}
