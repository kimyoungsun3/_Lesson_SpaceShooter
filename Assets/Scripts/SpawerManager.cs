using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpawerManager : MonoBehaviour {	
	public static SpawerManager ins;
	public Transform[] spawnPoint;
	public float spawnTimeBetween;

	void Awake(){
		ins = this;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine( SpawnEnemy ());
	}

	IEnumerator SpawnEnemy(){
		Transform _t;
		int i;
		int _len = spawnPoint.Length;
		GameObject _obj;
		Asteriod _scp;
		spawnPoint = Utility.ShufflArray<Transform> (spawnPoint);
		while (true) {
			for (i = 0; i < _len; i++) {
				//array -> shuffle(매번 들어오면)...
				_t = spawnPoint[i];

				if (i == 0) {
					PoolManager.ins.Instantiate ("Enemy", _t.position, Quaternion.identity);
				} else {
					PoolManager.ins.Instantiate ("Asteriod3", _t.position, Quaternion.identity);
				}
			}
			spawnPoint = Utility.ShufflArray<Transform> (spawnPoint);
			yield return new WaitForSeconds (spawnTimeBetween);
		}
	}

}
