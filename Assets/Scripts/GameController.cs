using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour {	
	public Transform[] spawnPoint;
	public float spawnTimeBetween;
	public int seed;
	public bool bDebug;
	//float spawnTime;
	//Queue<Transform> shuffleIndex;

	int score;
	public Text scoreText;
	public Text gameoverText;
	public GameObject btnRestart;

	int nameNum = 0;

	// Use this for initialization
	void Start () {
		StartCoroutine( SpawnEnemy ());

		SoundManager.ins.Play ("music_background");

		gameoverText.gameObject.SetActive(false);
		btnRestart.SetActive (false);
		score = 0;
		SetScore (0);
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
					//Debug.Log (_t.position);
					PoolManager.ins.Instantiate ("Asteroid2", _t.position, Quaternion.identity);
				} else {
					PoolManager.ins.Instantiate ("Asteroid", _t.position, Quaternion.identity);
				}
				//PoolManager.ins.Instantiate ("Asteroid", _t.position, Quaternion.identity);
			}
			spawnPoint = Utility.ShufflArray<Transform> (spawnPoint);
			yield return new WaitForSeconds (spawnTimeBetween);
		}
	}

	//GameConstroller.ins.SetScore(1);
	public void SetScore(int _score){
		score += _score;
		scoreText.text = "Score : " + score.ToString ();
	}

	public void GameOver(){
		//Debug.Log ("#### game over");
		gameoverText.gameObject.SetActive(true);
		btnRestart.SetActive(true);
	}

	public void InvokeRestart(){
		Application.LoadLevel (0);
	}
}
