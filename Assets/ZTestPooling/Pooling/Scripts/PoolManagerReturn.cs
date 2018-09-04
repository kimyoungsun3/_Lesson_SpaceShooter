using UnityEngine;
using System.Collections;

public class PoolManagerReturn: MonoBehaviour {
	public float destroyTime = 5f;
	void OnEnable(){
		Invoke ("Destroy", destroyTime);
	}

	void Destroy(){
		gameObject.SetActive (false);
	}

	void OnDisable(){
		CancelInvoke ();
	}
}
