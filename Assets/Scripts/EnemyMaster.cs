using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour {
	protected Rigidbody rb;
	protected Transform trans;
	public float tumble = 10f;
	public float moveSpeed = 3f;

	protected virtual void Start () {
		trans 	= transform;
		rb 		= GetComponent<Rigidbody> ();
	}

	protected void OnEnable(){
		bDie = false;
		health = HEALTH_MAX;
	}

	// Update is called once per frame
	void Update () {
		//아래 방향으로 이동하기..(회전축이 변화해서 World공간을 기준으로 이동)
		trans.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);		
	}

	public float HEALTH_MAX  = 1f;
	protected float health;
	protected bool bDie = false;
	public virtual void HitDamage(float _damage, Vector3 _hitPoint){

		health -= _damage;
		PoolManager.ins.Instantiate("explosion_hit", _hitPoint, Quaternion.identity);

		if (!bDie && health <= 0) {
			Die ();
		}
	}

	protected virtual void Die(){
		PoolManager.ins.Instantiate("explosion_asteroid", trans.position, trans.rotation);
		bDie = true;
		OnDestroy();
	}

	protected void OnDestroy(){
		gameObject.SetActive(false);
	}
}
