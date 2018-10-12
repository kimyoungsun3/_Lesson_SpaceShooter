using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyMaster {
	
	//X
	void OnTriggerEnter(Collider _col){		
		if (_col.CompareTag ("Boundary")) {
			//아래 경계라인에 충돌..
			OnDestroy();
		} else if (_col.CompareTag ("Player")) {
			//플레이어와 충돌..
			PoolManager.ins.Instantiate("explosion_asteroid", trans.position, trans.rotation);

			PlayerController _scpPlayer = _col.transform.parent.GetComponent<PlayerController> ();
			if (_scpPlayer != null) {
				_scpPlayer.HitDamage (1, trans.position);
			}
			OnDestroy ();
		}
	}

	protected override void Die(){
		SpawerManager.ins.SetScore (10);
		base.Die ();
	}


}
