using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour {
	[Serializable]
	public class SoundData{
		public AudioClip clip;
		public int channel = 1;
		public string name; 
	}

	private static SoundManager ins_;
	public static SoundManager ins{
		get{
			//Debug.Log ("SoundManager ins create");
			return ins_;
		}
		set{ ins_ = value; }
	}
	public float pitchLower = 0.95f;
	public float pitchUp = 1.05f;
	public AudioSource[] music;
	public SoundData[] soundData;
	private SoundData beforeData;
	bool bInit = false;
	int nameNum = 0;

	void Awake(){
		gameObject.name += ++nameNum;
		//Debug.Log ("SoundManager Awake ins_[" + ins_ + "]" +nameNum);
		if (ins == null) {
			//Debug.Log ("first create");
			ins = this;
		} else if (ins_ != this) {
			//전것존재 -> 또다른것 -> 삭제. 이후는 실행안됨(Start, OnEnable)...
			//Debug.Log ("또생성? 음... 삭제(지금것)");
			Destroy (gameObject);
			return;
		}
		DontDestroyOnLoad (gameObject);

		if (!bInit) {
			bInit = true;
			music [0].loop = true;
		}
	}

	public void Play(string _name){
		//Debug.Log ("SoundManager Play");
		if (beforeData != null && beforeData.name.Equals (_name)) {
			//before data reuse.
			beforeData = beforeData;
		}else{
			//find
			beforeData = FindSound (_name);
		}

		if (beforeData != null) {
			music [beforeData.channel].pitch = Random.Range (pitchLower, pitchUp);
			music [beforeData.channel].Stop ();
			music [beforeData.channel].clip = beforeData.clip;
			music [beforeData.channel].Play ();
		}
	}

	public void Stop(int _channel){
		music [_channel].Stop ();
	}

	private SoundData FindSound(string _name){
		List<SoundData> _temp = new List<SoundData> ();
		_temp.Clear ();

		for (int i = 0; i < soundData.Length; i++) {
			if (_name.Equals (soundData [i].name)) {
				_temp.Add (soundData[i]);
			}
		}

		if (_temp.Count <= 0) {
			Debug.LogError ("사운드 이름 없음:" + _name);
			return null;
		} else {
			return _temp [Random.Range (0, _temp.Count)];
		}
	}


}
