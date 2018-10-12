using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteriod : EnemyMaster {

	protected override void Start(){
		base.Start ();
		rb.angularVelocity = Random.insideUnitSphere * tumble;
		health = HEALTH_MAX;
	}


	//X
	void OnTriggerEnter(Collider _col){		
		if (_col.CompareTag ("Boundary")) {
			//아래 경계라인에 충돌..
			//Debug.Log ("Asteriod 경계와 충돌...");
			OnDestroy();
		} else if (_col.CompareTag ("Player")) {
			//플레이어와 충돌..
			//Debug.Log ("Asteriod Player와 충돌...");
			PoolManager.ins.Instantiate("explosion_asteroid", trans.position, trans.rotation);

			PlayerController _scpPlayer = _col.transform.parent.GetComponent<PlayerController> ();
			if (_scpPlayer != null) {
				_scpPlayer.HitDamage (1, trans.position);
			}
			OnDestroy ();
		}
	}

	public override void HitDamage (float _damage, Vector3 _hitPoint)
	{
		//Debug.Log ("Asteriod");
		base.HitDamage (_damage, _hitPoint);
	}

	protected override void Die(){
		SpawerManager.ins.SetScore (1);
		base.Die ();
	}

}
