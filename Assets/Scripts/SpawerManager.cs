using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpawerManager : MonoBehaviour {	
	public static SpawerManager ins { set; get; }
	public Transform[] spawnPoint;
	public float spawnTimeBetween;

	int score;
	public Text scoreText;
	public Text gameoverText;
	public GameObject btnRestart;

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
		while (true) {
			spawnPoint = Utility.ShufflArray<Transform> (spawnPoint);
			for (i = 0; i < _len; i++) {
				//array -> shuffle(매번 들어오면)...
				_t = spawnPoint[i];

				if (i == 0) {
					PoolManager.ins.Instantiate ("Enemy", _t.position, _t.rotation);
				} else {
					PoolManager.ins.Instantiate ("Asteriod3", _t.position, _t.rotation);
				}
			}
			yield return new WaitForSeconds (spawnTimeBetween);
		}
	}

	public void SetScore(int _score){
		score += _score;
		scoreText.text = "Score : " + score.ToString ();
	}

	public void GameOver(){
		gameoverText.gameObject.SetActive(true);
		btnRestart.SetActive(true);
	}

	public void InvokeRestart(){
		Application.LoadLevel (0);
	}
}
